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

		[DllImport(DLL_CORE, EntryPoint = "NewMagickWand")]
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

	}
}
