using System;
using System.Runtime.InteropServices;

using size_t = System.UInt64;
using MagickSizeType = System.UInt64;
using MagickBooleanType = System.Boolean;
using Quantum = System.UInt16;

namespace MagickSharp
{
	/// <summary>
	/// This class directly mirrors the MagickWand API. 
	/// Method signatures and documentation are mostly identical to what 
	/// can be found on the ImageMagick website.
	/// </summary>
	public static class ImageMagick
	{
		/// <summary>
		/// The name of the DLL to load the MagicWand API from.
		/// </summary>
		public const string DLL_CORE = "CORE_RL_wand_.dll";


		/*******************************************************************
		 ********************* Magick Wand Methods *************************
		 ******************************************************************/
		/// <summary>
		/// MagickWandGenesis() initializes the MagickWand environment.
		/// </summary>
		[DllImport(DLL_CORE)]
		public static extern void MagickWandGenesis();

		/// <summary>
		/// MagickWandTerminus() terminates the MagickWand environment.
		/// </summary>
		[DllImport(DLL_CORE)]
		public static extern void MagickWandTerminus();

		/// <summary>
		/// NewMagickWand() returns a wand required for all other methods in the API. A fatal exception is thrown if there is not enough memory to allocate the wand. Use DestroyMagickWand() to dispose of the wand when it is no longer needed.
		/// </summary>
		/// <returns></returns>
		public static MagickWand NewMagickWand() { return new MagickWand(NewMagickWandImpl()); }

		[DllImport(DLL_CORE, EntryPoint="NewMagickWand")]
		private static extern IntPtr NewMagickWandImpl();

		/// <summary>
		/// DestroyMagickWand() deallocates memory associated with a MagickWand.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		public static MagickWand DestroyMagickWand(MagickWand wand) { wand.Dispose(); return wand; }

		[DllImport(DLL_CORE, EntryPoint = "DestroyMagickWand")]
		private static extern IntPtr DestroyMagickWandImpl(IntPtr wand);


		/// <summary>
		/// ClearMagickWand() clears resources associated with the wand.
		/// </summary>
		/// <param name="wand"></param>
		[DllImport(DLL_CORE)]
		public static extern void ClearMagickWand(MagickWand wand);

		/// <summary>
		/// CloneMagickWand() makes an exact copy of the specified wand.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickWand CloneMagickWand(MagickWand wand);

		/// <summary>
		/// IsMagickWand() returns MagickTrue if the wand is verified as a magick wand.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickBooleanType IsMagickWand(MagickWand wand);

		/// <summary>
		/// MagickGetException() returns the severity, reason, and description of any error that occurs when using other methods in this API.
		/// </summary>
		/// <param name="wand"></param>
		/// <param name="severity"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern string MagickGetException(MagickWand wand, out ExceptionType severity);

		/// <summary>
		/// MagickGetExceptionType() returns the exception type associated with the wand. If no exception has occurred, UndefinedExceptionType is returned.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern ExceptionType MagickGetExceptionType(MagickWand wand);

		/// <summary>
		/// MagickGetIteratorIndex() returns the position of the iterator in the image list.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern size_t MagickGetIteratorIndex(MagickWand wand);

		/// <summary>
		/// MagickQueryConfigureOption() returns the value associated with the specified configure option.
		/// </summary>
		/// <param name="option"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern string MagickQueryConfigureOption(string option);



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



		[StructLayout(LayoutKind.Sequential)]
		public struct MagickWand : IDisposable
		{
			private IntPtr wand;

			internal MagickWand(IntPtr wand)
			{
				this.wand = wand;
			}

			public void Dispose()
			{
				this.wand = ImageMagick.DestroyMagickWandImpl(this.wand);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PixelWand : IDisposable
		{
			private IntPtr wand;

			internal PixelWand(IntPtr wand)
			{
				this.wand = wand;
			}

			public void Dispose()
			{
				this.wand = ImageMagick.DestroyPixelWandImpl(this.wand);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Image
		{
			private IntPtr img;

			public Image(IntPtr i)
			{
				this.img = i;
			}
		}

		public enum ExceptionType
		{
			UndefinedException, WarningException = 300, ResourceLimitWarning = 300, TypeWarning = 305,
			OptionWarning = 310, DelegateWarning = 315, MissingDelegateWarning = 320, CorruptImageWarning = 325,
			FileOpenWarning = 330, BlobWarning = 335, StreamWarning = 340, CacheWarning = 345,
			CoderWarning = 350, FilterWarning = 352, ModuleWarning = 355, DrawWarning = 360,
			ImageWarning = 365, WandWarning = 370, RandomWarning = 375, XServerWarning = 380,
			MonitorWarning = 385, RegistryWarning = 390, ConfigureWarning = 395, PolicyWarning = 399,
			ErrorException = 400, ResourceLimitError = 400, TypeError = 405, OptionError = 410,
			DelegateError = 415, MissingDelegateError = 420, CorruptImageError = 425, FileOpenError = 430,
			BlobError = 435, StreamError = 440, CacheError = 445, CoderError = 450,
			FilterError = 452, ModuleError = 455, DrawError = 460, ImageError = 465,
			WandError = 470, RandomError = 475, XServerError = 480, MonitorError = 485,
			RegistryError = 490, ConfigureError = 495, PolicyError = 499, FatalErrorException = 700,
			ResourceLimitFatalError = 700, TypeFatalError = 705, OptionFatalError = 710, DelegateFatalError = 715,
			MissingDelegateFatalError = 720, CorruptImageFatalError = 725, FileOpenFatalError = 730, BlobFatalError = 735,
			StreamFatalError = 740, CacheFatalError = 745, CoderFatalError = 750, FilterFatalError = 752,
			ModuleFatalError = 755, DrawFatalError = 760, ImageFatalError = 765, WandFatalError = 770,
			RandomFatalError = 775, XServerFatalError = 780, MonitorFatalError = 785, RegistryFatalError = 790,
			ConfigureFatalError = 795, PolicyFatalError = 799
		}

		public enum FilterTypes
		{
			UndefinedFilter, PointFilter, BoxFilter, TriangleFilter, HermiteFilter,
			HanningFilter, HammingFilter, BlackmanFilter, GaussianFilter, QuadraticFilter,
			CubicFilter, CatromFilter, MitchellFilter, JincFilter, SincFilter, SincFastFilter,
			KaiserFilter, WelshFilter, ParzenFilter, BohmanFilter, BartlettFilter,
			LagrangeFilter, LanczosFilter, LanczosSharpFilter, Lanczos2Filter, Lanczos2SharpFilter,
			RobidouxFilter, SentinelFilter 
		}
	}
}
