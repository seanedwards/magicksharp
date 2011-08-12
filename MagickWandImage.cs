using System;
using System.Runtime.InteropServices;

using size_t = System.UInt32;
using ssize_t = System.Int32;
using MagickSizeType = System.UInt32;
using MagickBooleanType = System.Boolean;
using Quantum = System.UInt16;

namespace MagickSharp
{
	public static partial class ImageMagick
	{
		/*******************************************************************
		 ****************** Magick Wand Image Methods **********************
		 ******************************************************************/

		/// <summary>
		/// MagickNewImage() adds a blank image canvas of the specified size and background color to the wand.
		/// </summary>
		/// <param name="wand">The magick wand</param>
		/// <param name="columns">The image width</param>
		/// <param name="rows">The image height</param>
		/// <param name="background">The image color</param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickNewImage(MagickWand wand, size_t columns, size_t rows, PixelWand background);

		/// <summary>
		/// MagickReadImage() reads an image or image sequence. 
		/// The images are inserted at the current image pointer position. 
		/// Use MagickSetFirstIterator(), MagickSetLastIterator, or MagickSetImageIndex() 
		/// to specify the current image pointer position at the beginning of the image list, 
		/// the end, or anywhere in-between respectively.
		/// </summary>
		/// <param name="wand">the magick wand.</param>
		/// <param name="filename">the image filename.</param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickReadImage(MagickWand wand, [MarshalAs(UnmanagedType.LPStr)] string filename);

		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickReadImageBlob(MagickWand wand, IntPtr blob, size_t length);

		/// <summary>
		/// MagickWriteImage() writes an image to the specified filename. 
		/// If the filename parameter is NULL, the image is written to the filename 
		/// set by MagickReadImage() or MagickSetImageFilename().
		/// </summary>
		/// <param name="wand">the magick wand.</param>
		/// <param name="filename">the image filename.</param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickWriteImage(MagickWand wand, [MarshalAs(UnmanagedType.LPStr)] string filename);

		/// <summary>
		/// MagickRotateImage() rotates an image the specified number of degrees. 
		/// Empty triangles left over from rotating the image are filled with the background color.
		/// </summary>
		/// <param name="wand">the magick wand.</param>
		/// <param name="background">the background pixel wand.</param>
		/// <param name="degrees">the number of degrees to rotate the image.</param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickRotateImage(MagickWand wand, PixelWand background, double degrees);

		/// <summary>
		/// Creates an ImageMagick wand from a System.Drawing.Bitmap.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="bmp"></param>
		public static void MagickReadImage(MagickWand wand, System.Drawing.Bitmap bmp)
		{
			System.Drawing.Imaging.BitmapData bmpdat = bmp.LockBits(
				new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			PixelWand pwand = ImageMagick.NewPixelWand();
			ImageMagick.PixelSetRGB(pwand, 1, 1, 1);

			MagickNewImage(wand, (size_t)bmp.Height, (size_t)bmp.Width, pwand);

			ImageMagick.DestroyPixelWand(pwand);

			MagickImportImagePixels(wand, 0, 0, (size_t)bmpdat.Width, (size_t)bmpdat.Height, 
				"ARGB", StorageType.CharPixel, bmpdat.Scan0);

			bmp.UnlockBits(bmpdat);
		}

		/// <summary>
		/// Gets a System.Drawing.Bitmap from an ImageMagick wand.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		public static System.Drawing.Bitmap MagickGetImageBitmap(MagickWand wand)
		{
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(
				(int)MagickGetImageWidth(wand), (int)MagickGetImageHeight(wand));

			System.Drawing.Imaging.BitmapData bmpdat = bmp.LockBits(
				new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
				System.Drawing.Imaging.ImageLockMode.ReadOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			MagickExportImagePixels(wand, 0, 0, MagickGetImageWidth(wand), MagickGetImageHeight(wand),
				"ARGB", StorageType.CharPixel, bmpdat.Scan0);

			return bmp;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		private static extern IntPtr MagickGetImageBlob(MagickWand wand, out size_t length);

		/// <summary>
		/// MagickImportImagePixels() accepts pixel datand stores it in the image at the location you specify. The method returns MagickFalse on success otherwise MagickTrue if an error is encountered. The pixel data can be either char, short int, int, ssize_t, float, or double in the order specified by map.
		///
		/// Suppose your want to upload the first scanline of a 640x480 image from character data in red-green-blue order:
		/// MagickImportImagePixels(wand,0,0,640,1,"RGB",CharPixel,pixels);
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="columns"></param>
		/// <param name="rows"></param>
		/// <param name="map">This string reflects the expected ordering of the pixel array. It can be any combination or order of R = red, G = green, B = blue, A = alpha (0 is transparent), O = opacity (0 is opaque), C = cyan, Y = yellow, M = magenta, K = black, I = intensity (for grayscale), P = pad.</param>
		/// <param name="storage">Define the data type of the pixels. Float and double types are expected to be normalized [0..1] otherwise [0..QuantumRange]. Choose from these types: CharPixel, ShortPixel, IntegerPixel, LongPixel, FloatPixel, or DoublePixel.</param>
		/// <param name="pixels">This array of values contain the pixel components as defined by map and type. You must preallocate this array where the expected length varies depending on the values of width, height, map, and type.</param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickImportImagePixels(MagickWand wand,
			ssize_t x, ssize_t y,size_t columns, size_t rows, 
			[MarshalAs(UnmanagedType.LPStr)] string map, 
			StorageType storage, IntPtr pixels);

		/// <summary>
		/// MagickExportImagePixels() extracts pixel data from an image and returns it to you. The method returns MagickTrue on success otherwise MagickFalse if an error is encountered. The data is returned as char, short int, int, ssize_t, float, or double in the order specified by map.
		///
		/// Suppose you want to extract the first scanline of a 640x480 image as character data in red-green-blue order:
		/// MagickExportImagePixels(wand,0,0,640,1,"RGB",CharPixel,pixels);
		/// </summary>
		/// <param name="wand">the magick wand.</param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="columns"></param>
		/// <param name="rows"></param>
		/// <param name="map">This string reflects the expected ordering of the pixel array. It can be any combination or order of R = red, G = green, B = blue, A = alpha (0 is transparent), O = opacity (0 is opaque), C = cyan, Y = yellow, M = magenta, K = black, I = intensity (for grayscale), P = pad.</param>
		/// <param name="storage">Define the data type of the pixels. Float and double types are expected to be normalized [0..1] otherwise [0..QuantumRange]. Choose from these types: CharPixel, DoublePixel, FloatPixel, IntegerPixel, LongPixel, QuantumPixel, or ShortPixel.</param>
		/// <param name="pixels">This array of values contain the pixel components as defined by map and type. You must preallocate this array where the expected length varies depending on the values of width, height, map, and type.</param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickExportImagePixels(MagickWand wand,
			ssize_t x, ssize_t y, size_t columns, size_t rows,
			[MarshalAs(UnmanagedType.LPStr)] string map,
			StorageType storage, IntPtr pixels);

		/// <summary>
		/// MagickGetImageFormat() returns the format of a particular image in a sequence.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static extern string MagickGetImageFormat(MagickWand wand);

		/// <summary>
		/// MagickSetImageFormat() sets the format of a particular image in a sequence.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="format"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType MagickSetImageFormat(MagickWand wand,
			[MarshalAs(UnmanagedType.LPStr)] string format);

		/// <summary>
		/// MagickGetImageHeight() returns the image height.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern size_t MagickGetImageHeight(MagickWand wand);

		/// <summary>
		/// MagickGetImageHeight() returns the image width.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern size_t MagickGetImageWidth(MagickWand wand);

	}
}
