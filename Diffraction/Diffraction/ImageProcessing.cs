/*
 * Created by SharpDevelop.
 * User: Азриэль
 * Date: 10/15/2021
 * Time: 15:30
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace Diffraction
{
	/// <summary>
	/// Description of ImageProcessing.
	/// </summary>
	public static class ImageProcessing
	{		
		public static BitmapImage FromFile(string filename)
		{
			var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
			var image = new BitmapImage();
			image.BeginInit();
			fs.Seek(0, SeekOrigin.Begin);
			image.StreamSource = fs;
			image.EndInit();
			return image;
		}
		
		public static BitmapImage ConvertToSource(Bitmap src)
		{
			var ms = new MemoryStream();
            src.Save(ms, ImageFormat.Png);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
		}
		
		public static Bitmap ConvertToBitmap(BitmapImage src)
		{
			int width = src.PixelWidth, height = src.PixelHeight;
		    int stride = width * ((src.Format.BitsPerPixel + 7) / 8);
		    var ptr = IntPtr.Zero;
		    try {
		        ptr = Marshal.AllocHGlobal(height * stride);
		        src.CopyPixels(new System.Windows.Int32Rect(0, 0, width, height), ptr, height * stride, stride);
		        using (var btm = new Bitmap(width, height, stride, PixelFormat.Canonical, ptr)) {
		            // Clone the bitmap so that we can dispose it and
		            // release the unmanaged memory at ptr:
		            return new Bitmap(btm);
		        }
		    }
		    finally {
		        if (ptr != IntPtr.Zero)
		            Marshal.FreeHGlobal(ptr);
		    }
		}
		
		/// <summary>
		/// Инвертировать цвета изображения.
		/// </summary>
		/// <param name="Source">Исходное изображение.</param>
		public static Bitmap InvertColor(Bitmap Source)
		{
			int width = Source.Width, height = Source.Height;
			int depth = Image.GetPixelFormatSize(Source.PixelFormat) / 8;
			var pixeldata = Source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, Source.PixelFormat);
			int stride = pixeldata.Stride;
			var pixelfunc = ImageSignal.SelectPixelFunc(depth, stride);
			
			var buffer0 = new byte[height * stride];
			var buffer1 = new byte[width * height * 4];
			Marshal.Copy(pixeldata.Scan0, buffer0, 0, buffer0.Length);
			
			System.Threading.Tasks.Parallel.For(0, width, i => {
				System.Threading.Tasks.Parallel.For(0, height, j => {
			        int v = Math.Min(255, Math.Max(0, pixelfunc(i, j, buffer0)));
			        int k = j * width + i;
			        buffer1[4 * k] = buffer1[4 * k + 1] = buffer1[4 * k + 2] = (byte)(255 - v);
			        buffer1[4 * k + 3] = 255;
			    });
			});
			Source.UnlockBits(pixeldata);
			
//			var Result = new Bitmap(Source.Width, Source.Height, Source.PixelFormat);
//			for (int i = 0; i < Source.Width; i++) {
//				for (int j = 0; j < Source.Height; j++) {
//					var v = Source.GetPixel(i, j);
//					Result.SetPixel(i, j, Color.FromArgb(255, 255 - v.R, 255 - v.G, 255 - v.B));
//				}
//			}
			
			var Result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			var resultdata = Result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, Result.PixelFormat);
			Marshal.Copy(buffer1, 0, resultdata.Scan0, buffer1.Length);
			Result.UnlockBits(resultdata);
			return Result;
		}
		
		/// <summary>
		/// НЕ ИСПОЛЬЗУЕТСЯ: преобразовать изображение в чёрно-белое.
		/// </summary>
		/// <param name="Source">Исходное изображение.</param>
		/// <param name="AlphaBg">Яркость фона для альфа-канала.</param>
		public static Bitmap Grayscale(Bitmap Source, byte AlphaBg = 255)
		{
			var Result = new Bitmap(Source.Width, Source.Height, PixelFormat.Canonical);
			for (int i = 0; i < Source.Width; i++) {
				for (int j = 0; j < Source.Height; j++) {
					byte v = GrayValue(Source.GetPixel(i, j), AlphaBg);
					Result.SetPixel(i, j, Color.FromArgb(255, v, v, v));
				}
			}
			return Result;
		}
		
		/// <summary>
		/// НЕ ИСПОЛЬЗУЕТСЯ: значение яркости (ч/б) цвета.
		/// </summary>
		/// <param name="Base">Цвет.</param>
		/// <param name="AlphaBg">Яркость фона для альфа-канала.</param>
		public static byte GrayValue(Color Base, byte AlphaBg = 255) {
			double v = 0.299 * Base.R + 0.587 * Base.G + 0.114 * Base.B;
			if (Base.A < 255)
				v = v * Base.A / 255.0 + AlphaBg * (255 - Base.A) / 255.0;
			return (byte)v;
		}
	}
}
