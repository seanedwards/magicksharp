﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MagickSharp.Test
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class ImageMagick
	{
		public ImageMagick()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		bool NoException(MagickSharp.ImageMagick.MagickWand wand)
		{
			return MagickSharp.ImageMagick.MagickGetExceptionType(wand) ==
				MagickSharp.ImageMagick.ExceptionType.UndefinedException;
		}

		void AssertNoErrors(MagickSharp.ImageMagick.MagickWand wand)
		{
			MagickSharp.ImageMagick.ExceptionType type;
			Assert.IsTrue(this.NoException(wand),
				MagickSharp.ImageMagick.MagickGetException(wand, out type));
		}

		[ClassInitialize]
		public static void Genesis(TestContext testContext)
		{
			System.IO.Directory.CreateDirectory("TestResults");
			MagickSharp.ImageMagick.MagickWandGenesis();
		}

		[ClassCleanup]
		public static void Terminus()
		{
			MagickSharp.ImageMagick.MagickWandTerminus();
		}

		[TestInitialize]
		public void CreateWand()
		{
			wand = MagickSharp.ImageMagick.NewMagickWand();
			this.AssertNoErrors(wand);
		}

		[TestCleanup]
		public void DestroyWand()
		{
			wand = MagickSharp.ImageMagick.DestroyMagickWand(wand);
		}

		MagickSharp.ImageMagick.MagickWand wand;


		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void MagickNewImage()
		{
			MagickSharp.ImageMagick.PixelWand pxwand = MagickSharp.ImageMagick.NewPixelWand();
			MagickSharp.ImageMagick.PixelSetRGB(pxwand, 0, 0, 0);
			MagickSharp.ImageMagick.MagickNewImage(wand, 320, 240, pxwand);
			this.AssertNoErrors(wand);

			MagickSharp.ImageMagick.MagickWriteImage(wand, "TestResults\\MagickNewImage.png");
			this.AssertNoErrors(wand);
		}

		[TestMethod]
		public void MagickReadWriteImage()
		{
			MagickSharp.ImageMagick.MagickReadImage(wand, "..\\..\\..\\MagickSharp.Test.Data\\wizard.png");
			this.AssertNoErrors(wand);

			MagickSharp.ImageMagick.MagickWriteImage(wand, "TestResults\\MagickReadWriteImage.wizard.png");
			this.AssertNoErrors(wand);
		}

		[TestMethod]
		public void MagickRotateImage()
		{
			MagickSharp.ImageMagick.MagickReadImage(wand, "..\\..\\..\\MagickSharp.Test.Data\\wizard.png");
			this.AssertNoErrors(wand);

			MagickSharp.ImageMagick.PixelWand pxwand = MagickSharp.ImageMagick.NewPixelWand();
			MagickSharp.ImageMagick.PixelSetRGB(pxwand, 0, 0, 0);

			MagickSharp.ImageMagick.MagickRotateImage(wand, pxwand, 45);
			this.AssertNoErrors(wand);

			MagickSharp.ImageMagick.MagickWriteImage(wand, "TestResults\\MagickRotateImage.wizard.png");
			this.AssertNoErrors(wand);
		}

		[TestMethod]
		public void MagickBitmapInterop()
		{
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap("..\\..\\..\\MagickSharp.Test.Data\\wizard.png");

			MagickSharp.ImageMagick.MagickReadImage(wand, bmp);
			this.AssertNoErrors(wand);

			System.Drawing.Bitmap bmp2 = MagickSharp.ImageMagick.MagickGetImageBitmap(wand);
			this.AssertNoErrors(wand);

			bmp.Save("TestResults\\MagickBitmapInterop.wizard.png");
		}
	}
}
