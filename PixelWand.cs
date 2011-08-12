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
		 ********************** Pixel Wand Methods ************************
		 ******************************************************************/

		/// <summary>
		/// NewPixelWand() returns a new pixel wand.
		/// </summary>
		/// <returns></returns>
		public static PixelWand NewPixelWand() { return new PixelWand(NewPixelWandImpl()); }

		[DllImport(DLL_CORE, EntryPoint = "NewPixelWand")]
		private static extern IntPtr NewPixelWandImpl();

		/// <summary>
		/// DestroyPixelWand() deallocates resources associated with a PixelWand.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		public static PixelWand DestroyPixelWand(PixelWand wand) { wand.Dispose(); return wand; }

		[DllImport(DLL_CORE, EntryPoint = "DestroyPixelWand")]
		private static extern IntPtr DestroyPixelWandImpl(IntPtr wand);

		/// <summary>
		/// PixelSetRed() sets the normalized red color of the pixel wand.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="c"></param>
		[DllImport(DLL_CORE)]
		public static extern void PixelSetRed(PixelWand wand, double c);

		/// <summary>
		/// PixelSetGreen() sets the normalized red color of the pixel wand.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="c"></param>
		[DllImport(DLL_CORE)]
		public static extern void PixelSetGreen(PixelWand wand, double c);

		/// <summary>
		/// PixelSetBlue() sets the normalized red color of the pixel wand.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="c"></param>
		[DllImport(DLL_CORE)]
		public static extern void PixelSetBlue(PixelWand wand, double c);

		/// <summary>
		/// PixelSetRGB() sets the red, green and blue colors of the pixel wand simultaneously.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		public static void PixelSetRGB(PixelWand wand, double r, double g, double b)
		{
			PixelSetRed(wand, r); PixelSetGreen(wand, g); PixelSetBlue(wand, b);
		}

		/// <summary>
		/// PixelSetOpacity() sets the normalized opacity color of the pixel wand.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="opacity"></param>
		[DllImport(DLL_CORE)]
		public static extern void PixelSetOpacity(PixelWand wand, double opacity);
	}
}
