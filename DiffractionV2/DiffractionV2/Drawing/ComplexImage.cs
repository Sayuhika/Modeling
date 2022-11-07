/*
 * Created by SharpDevelop.
 * User: Азриэль
 * Date: 10/15/2021
 * Time: 15:15
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DiffractionV2.Drawing
{
	/// <summary>
	/// Комплекснозначное изображение (двумерный сигнал).
	/// </summary>
	public class ComplexImage
	{
		/// <summary>
		/// Отсчёты сигнала.
		/// </summary>
		public Complex[,] Data;
		
		/// <summary>
		/// Является ли этот сигнал спектром (представлением в обратном пространстве).
		/// </summary>
		public bool IsSpectrum;
		
		/// <summary>
		/// Отклонение от нуля мнимой части при обратном преобразовании Фурье.
		/// </summary>
		public double ImagPart;
		public double MaxV;
		public ComplexImage() {}
		
		/// <summary>
		/// Создать объект на основе точечного изображения.
		/// </summary>
		/// <param name="Source">Исходное изображение.</param>
		public ComplexImage(Bitmap Source)
		{
			int width = Source.Width, height = Source.Height;
			int depth = Image.GetPixelFormatSize(Source.PixelFormat) / 8;
			var pixeldata = Source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Source.PixelFormat);
			var pixelfunc = ImageSignal.SelectPixelFunc(depth, pixeldata.Stride);
			
			var buffer = new byte[height * pixeldata.Stride];
			Marshal.Copy(pixeldata.Scan0, buffer, 0, buffer.Length);
			
			Data = new Complex[Source.Width, Source.Height];
			Parallel.For(0, width, (i) => {
			    Parallel.For(0, height, (j) => {
					Data[i, j] = Complex.One * pixelfunc(i, j, buffer) / 255.0;
			    });
			});
			Source.UnlockBits(pixeldata);
		}
		
		/// <summary>
		/// Получение точечного изображения, отражающего значения отсчётов сигнала.
		/// </summary>
		/// <param name="IsLogScale">Использовать логарифмический масштаб для спектра.</param>
		/// <returns>Если является спектром, то модуль отсчётов; иначе действительная часть отсчётов.</returns>
		public Bitmap ToBitmap(bool IsLogScale = false) 
		{
			int width = Data.GetLength(0), height = Data.GetLength(1);
			Func<int, int, double> vfunc;
			if (IsSpectrum) {
				if (IsLogScale) vfunc = (i, j) => {
					return Math.Log(Data[i,j].Magnitude + 1);
				};
				else vfunc = (i, j) => {
					return Data[i,j].Magnitude;
				};
			}
			else vfunc = (i, j) => Data[i, j].Magnitude;
			
			var buffer = new byte[width * height * 4];
			//Parallel.For(0, width, i => {
			//	Parallel.For(0, height, j => {
			for (int i = 0; i < width; i++) {
				for (int j = 0; j < height; j++) {
					byte v = (byte)(255.0 * (vfunc(i, j) - 0) / (MaxV - 0));
					int k = j * width + i;
					buffer[4 * k] = buffer[4 * k + 1] = buffer[4 * k + 2] = v;
					buffer[4 * k + 3] = 255;
				}
			}
			//	});
			//});
			
			var Result = new Bitmap(Data.GetLength(0), Data.GetLength(1), PixelFormat.Format32bppArgb);
			var pixelresult = Result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, Result.PixelFormat);
			Marshal.Copy(buffer, 0, pixelresult.Scan0, buffer.Length);
			Result.UnlockBits(pixelresult);
			return Result;
		}
	}
}
