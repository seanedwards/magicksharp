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
	public static partial class ImageMagick
	{
		/// <summary>
		/// The name of the DLL to load the MagicWand API from.
		/// </summary>
		public const string DLL_CORE = "CORE_RL_wand_.dll";


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

		public enum StorageType
		{
			UndefinedPixel, CharPixel, DoublePixel, FloatPixel, IntegerPixel, LongPixel, QuantumPixel, ShortPixel 
		}
	}
}
