using System;
using System.Runtime.InteropServices;

using size_t = System.UInt64;
using MagickSizeType = System.UInt64;
using MagickBooleanType = System.Boolean;
using Quantum = System.UInt16;

namespace MagickSharp
{
	/// <summary>
	/// 
	/// </summary>
	public static class ImageMagick
	{
		/// <summary>
		/// The name of the DLL to load the MagicWand API from.
		/// </summary>
		public const string DLL_CORE = "CORE_RL_wand_.dll";

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
		/// DestroyMagickWand() deallocates memory associated with a MagickWand.
		/// </summary>
		/// <param name="wand"></param>
		/// <returns></returns>
		[DllImport(DLL_CORE)]
		public static extern MagickWand DestroyMagickWand(MagickWand wand);

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

		[StructLayout(LayoutKind.Sequential)]
		public struct MagickWand
		{
			size_t id;

			[MarshalAs(UnmanagedType.LPStr)]
			string name;

			IntPtr exception;

			IntPtr image_info;

			IntPtr quantize_info;

			IntPtr images;

			MagickBooleanType active, pend, debug;

			size_t signature;
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
	}
}
