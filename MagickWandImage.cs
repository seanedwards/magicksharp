using System;
using System.Runtime.InteropServices;

using size_t = System.UInt64;
using MagickSizeType = System.UInt64;
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

	}
}
