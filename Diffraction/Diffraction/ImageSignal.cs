/*
 * Created by SharpDevelop.
 * User: Азриэль
 * Date: 17.10.2021
 * Time: 13:18
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Diffraction
{
	public static class ImageSignal
	{
		public static Func<int, int, byte[], int> SelectPixelFunc(int depth, int stride)
		{
			
			Func<int, int, byte[], int> pixelfunc = null;
			switch (depth) {
				case 1:
					pixelfunc = (i, j, v) => v[i * depth + j * stride];
					break;
				case 3:
					pixelfunc = (i, j, v) => (int)(0.114 * v[i * depth + j * stride] + 0.587 * v[i * depth + j * stride + 1] + 0.299 * v[i * depth + j * stride + 2]);
					break;
				case 4:
					pixelfunc = (i, j, v) => {
						double y = 0.114 * v[i * depth + j * stride] + 0.587 * v[i * depth + j * stride + 1] + 0.299 * v[i * depth + j * stride + 2];
						if (v[i * depth + j * stride + 3] < 255)
							y = y * v[i * depth + j * stride + 3] / 255.0 + (255 - v[i * depth + j * stride + 3]);
						return (byte)y;
					};
					break;
				default: throw new FormatException();
			}
			return pixelfunc;
		}
		
		/// <summary>
		/// Расширить изображение и заполнить пустые поля.
		/// </summary>
		/// <param name="Source">Исходное изображение.</param>
		/// <param name="NewSize">Размер нового изображения.</param>
		/// <param name="Fill">Значение для заполнения.</param>
		/// <returns>Расширенное квадратное изображение.</returns>
		public static Bitmap Padding(Bitmap Source, int NewSize, byte Fill = 0)
		{
			int width = Source.Width, height = Source.Height;
			int depth = Image.GetPixelFormatSize(Source.PixelFormat) / 8;
			var pixeldata = Source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Source.PixelFormat);
			var pixelfunc = ImageSignal.SelectPixelFunc(depth, pixeldata.Stride);
			
			var buffer0 = new byte[height * pixeldata.Stride];
			var buffer1 = new byte[NewSize * NewSize * 4];
			Marshal.Copy(pixeldata.Scan0, buffer0, 0, buffer0.Length);
			
			Parallel.For(0, NewSize, i => {
				Parallel.For(0, NewSize, j => {
					int v = (i < width && j < height ? pixelfunc(i, j, buffer0) : Fill);
					int k = j * NewSize + i;
					buffer1[4 * k] = buffer1[4 * k + 1] = buffer1[4 * k + 2] = (byte)v;
					buffer1[4 * k + 3] = 255;
			    });
			});
			Source.UnlockBits(pixeldata);
			
			var Result = new Bitmap(NewSize, NewSize, PixelFormat.Format32bppArgb);
			var pixelresult = Result.LockBits(new Rectangle(0, 0, NewSize, NewSize), ImageLockMode.WriteOnly, Result.PixelFormat);
			Marshal.Copy(buffer1, 0, pixelresult.Scan0, buffer1.Length);
			Result.UnlockBits(pixelresult);
			return Result;
		}
		
		/// <summary>
		/// Провести билинейную интерполяцию.
		/// </summary>
		/// <param name="Source">Исходное изображение.</param>
		/// <param name="NewSize">Размер нового изображения.</param>
		/// <returns>Интерполированное квадратное изображение.</returns>
		public static Bitmap Interpolate(Bitmap Source, int NewSize)
		{
			int width = Source.Width, height = Source.Height;
			int depth = Image.GetPixelFormatSize(Source.PixelFormat) / 8;
			var pixeldata = Source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Source.PixelFormat);
			var pixelfunc = ImageSignal.SelectPixelFunc(depth, pixeldata.Stride);
			
			var buffer0 = new byte[height * pixeldata.Stride];
			var buffer1 = new byte[NewSize * NewSize * 4];
			Marshal.Copy(pixeldata.Scan0, buffer0, 0, buffer0.Length);
			
			/*
			Parallel.For(0, NewSize, (j) => {
				double tmp = j / (double)(NewSize - 1) * (height - 1);
				int h = (int)Math.Floor(tmp);
				if (h < 0) h = 0;
				else if (h >= height - 1)
					h = height - 2;
				double u = tmp - h;
				
				Parallel.For(0, NewSize, (i) => {
					tmp = i / (double)(NewSize - 1) * (width - 1);
					int w = (int)Math.Floor(tmp);
					if (w < 0) w = 0;
					else if (w >= width - 1)
						w = width - 2;
					double t = tmp - w;
					
					// Коэффициенты:
					double d1 = (1 - t) * (1 - u);
					double d2 = t * (1 - u);
					double d3 = t * u;
					double d4 = (1 - t) * u;
					
					// Окрестные пиксели:
					int p1 = pixelfunc(w, h, buffer0);
					int p2 = pixelfunc(w + 1, h, buffer0);
					int p3 = pixelfunc(w + 1, h + 1, buffer0);
					int p4 = pixelfunc(w, h + 1, buffer0);
					
					// Интерполированное значение:
					double v = p1 * d1 + p2 * d2 + p3 * d3 + p4 * d4;
					int k = j * NewSize + i;
					buffer1[4 * k] = buffer1[4 * k + 1] = buffer1[4 * k + 2] = (byte)Math.Min(255, Math.Max(0, v));
					buffer1[4 * k + 3] = 255;
				});
			});
			*/
			
			Func<double, double, double, double> Lerp = (s, e, t) => s + (e - s) * t;
			Func<double, double, double, double, double, double, double> BLerp = (c00, c10, c01, c11, tx, ty) => Lerp(Lerp(c00, c10, tx), Lerp(c01, c11, tx), ty);
			Parallel.For(0, NewSize, x => {
				Parallel.For(0, NewSize, y => {
			    	double gx = (double)x / NewSize * (width - 1);
			    	double gy = (double)y / NewSize * (height - 1);
			    	int gxi = (int)gx, gyi = (int)gy;
			    	double v = BLerp(
			    		pixelfunc(gxi, gyi, buffer0),
			    		pixelfunc(gxi + 1, gyi, buffer0),
			    		pixelfunc(gxi, gyi + 1, buffer0),
			    		pixelfunc(gxi + 1, gyi + 1, buffer0),
			    		gx - gxi, gy - gyi);
			    	int k = y * NewSize + x;
			    	buffer1[4 * k] = buffer1[4 * k + 1] = buffer1[4 * k + 2] = (byte)Math.Min(255, Math.Max(0, v));
			    	buffer1[4 * k + 3] = 255;
			    });
			});
			Source.UnlockBits(pixeldata);
			
			var Result = new Bitmap(NewSize, NewSize, PixelFormat.Format32bppArgb);
			var pixelresult = Result.LockBits(new Rectangle(0, 0, NewSize, NewSize), ImageLockMode.WriteOnly, Result.PixelFormat);
			Marshal.Copy(buffer1, 0, pixelresult.Scan0, buffer1.Length);
			Result.UnlockBits(pixelresult);
			return Result;
		}
		
		/// <summary>
		/// Вычислить критерий сходства двух изображений.
		/// </summary>
		/// <returns>Среднеквадратичное отклонение.</returns>
		public static double Similarity(Bitmap Image1, Bitmap Image2)
		{
			if (Image1 == null || Image2 == null) throw new ArgumentNullException();
			if (Image1.Width != Image2.Width || Image1.Height != Image2.Height) throw new ArgumentException();
			int width = Image1.Width, height = Image1.Height;
			int depth1 = Image.GetPixelFormatSize(Image1.PixelFormat) / 8;
			int depth2 = Image.GetPixelFormatSize(Image2.PixelFormat) / 8;
			var data1 = Image1.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Image1.PixelFormat);
			var data2 = Image2.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Image2.PixelFormat);
			var pf1 = SelectPixelFunc(depth1, data1.Stride);
			var pf2 = SelectPixelFunc(depth2, data2.Stride);
			
			var buffer1 = new byte[height * data1.Stride];
			var buffer2 = new byte[height * data2.Stride];
			Marshal.Copy(data1.Scan0, buffer1, 0, buffer1.Length);
			Marshal.Copy(data2.Scan0, buffer2, 0, buffer2.Length);
			
			long total0 = 0, total1 = 0, total2 = 0, diff = 0;
			Parallel.For(0, width * height, (k) => {
			    int v1 = pf1(k % width, k / width, buffer1), v2 = pf2(k % width, k / width, buffer2);
			    Interlocked.Add(ref total0, (v1 + v2) * (v1 + v2) / 4);
			    Interlocked.Add(ref total1, v1 * v1);
			    Interlocked.Add(ref total2, v2 * v2);
			    Interlocked.Add(ref diff, (v1 - v2) * (v1 - v2));
			});
			Image1.UnlockBits(data1);
			Image2.UnlockBits(data2);
			
			// double struc = (double)total1 / (double)total2;
			double sim1 = 1.0 - (double)diff / (double)total1;
			double sim2 = 1.0 - (double)diff / (double)total2;
			return (sim1 > 0 ? sim1 : sim2);
		}
		
		/// <summary>
		/// Наложить аддитивный белый гауссов шум (АБГШ) на точечное изображение.
		/// </summary>
		/// <param name="Source">Исходное изображение.</param>
		/// <param name="NoiseMult">Коэффициент энергии шума.</param>
		/// <returns>Зашумлённое изображение.</returns>
		public static Bitmap ApplyNoise(Bitmap Source, double NoiseMult)
		{
			int width = Source.Width, height = Source.Height;
			int depth = Image.GetPixelFormatSize(Source.PixelFormat) / 8;
			var pixeldata = Source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Source.PixelFormat);
			var pixelfunc = ImageSignal.SelectPixelFunc(depth, pixeldata.Stride);
			
			var buffer0 = new byte[height * pixeldata.Stride];
			var buffer1 = new byte[width * height * 4];
			Marshal.Copy(pixeldata.Scan0, buffer0, 0, buffer0.Length);
			
			// Вычисление энергии сигнала и шума:
			long esignal = 0, enoise = 0;
			Parallel.For(0, width * height, (k) => {
				Interlocked.Add(ref esignal, pixelfunc(k % width, k / width, buffer0) * pixelfunc(k % width, k / width, buffer0));
				
				double noisev = 0;
				for (int q = 0; q < 12; q++) { noisev += ParallelRandom.NextDouble(); }
				noisev *= 255.0 / 12.0;
				Interlocked.Add(ref enoise, (int)(noisev * noisev));
				buffer1[4 * k] = (byte)noisev;
			});
			double noisek = Math.Sqrt(NoiseMult * esignal / enoise);
			
			// Наложение отсчётов шума на отсчёты сигнала:
			Parallel.For(0, width * height, (k) => {
				byte v = (byte)Math.Min(255, Math.Max(0, pixelfunc(k % width, k / width, buffer0) + noisek * (buffer1[4 * k] - 127)));
				buffer1[4 * k] = buffer1[4 * k + 1] = buffer1[4 * k + 2] = v;
				buffer1[4 * k + 3] = 255;
			});
			Source.UnlockBits(pixeldata);
			
			var Result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			var pixelresult = Result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, Result.PixelFormat);
			Marshal.Copy(buffer1, 0, pixelresult.Scan0, buffer1.Length);
			Result.UnlockBits(pixelresult);
			return Result;
		}
	}
	
	/// <summary>
	/// Генератор равномерно распределённых случайных чисел, модифицированный для использования в параллельных вычислениях.
	/// </summary>
	public static class ParallelRandom
	{
	    private static Random _global = new Random();
	    [ThreadStatic]
	    private static Random _local;
	    
	    /// <summary>
	    /// Привязанная к потоку инстанция Random. 
	    /// </summary>
	    private static Random inst {
	    	get {
	    		Random inst = _local;
	    		if (inst == null) {
	    			int seed;
	    			lock (_global) seed = _global.Next();
	    			_local = inst = new Random(seed);
	    		}
	    		return inst;
	    	}
	    }
	
	    /// <summary>
	    /// Сгенерировать случайное целое число от 0 до 2147483647.
	    /// </summary>
	    public static int Next() {
	        return inst.Next();
	    }
	    
	    /// <summary>
	    /// Сгенерировать случайное целое число в заданных пределах.
	    /// </summary>
	    /// <param name="min">Нижний предел ГСЧ.</param>
	    /// <param name="max">Верхний предел ГСЧ.</param>
	    public static int Next(int min, int max) {
	    	return inst.Next(min, max);
	    }
	    
	    /// <summary>
	    /// Сгенерировать случайное вещественное число от 0.0 до 1.0.
	    /// </summary>
	    public static double NextDouble() {
	    	return inst.NextDouble();
	    }
	}
}
