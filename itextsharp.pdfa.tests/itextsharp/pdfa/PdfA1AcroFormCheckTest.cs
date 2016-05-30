using System;
using System.IO;
using NUnit.Framework;
using iTextSharp.IO.Source;
using iTextSharp.Kernel.Pdf;
using iTextSharp.Kernel.Utils;
using iTextSharp.Test;

namespace iTextSharp.Pdfa
{
	public class PdfA1AcroFormCheckTest : ExtendedITextTest
	{
		public static readonly String sourceFolder = TestContext.CurrentContext.TestDirectory
			 + "/../../resources/itextsharp/pdfa/";

		public static readonly String cmpFolder = sourceFolder + "cmp/PdfA1AcroFormCheckTest/";

		public static readonly String destinationFolder = TestContext.CurrentContext.TestDirectory
			 + "/test/itextsharp/pdfa/PdfA1AcroFormCheckTest/";

		[TestFixtureSetUp]
		public static void BeforeClass()
		{
			CreateOrClearDestinationFolder(destinationFolder);
		}

		/// <exception cref="System.IO.FileNotFoundException"/>
		/// <exception cref="iTextSharp.Kernel.Xmp.XmpException"/>
		[Test]
		public virtual void AcroFormCheck01()
		{
			Assert.That(() => 
			{
				PdfWriter writer = new PdfWriter(new ByteArrayOutputStream());
				Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode
					.Open, FileAccess.Read);
				PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
					("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
				doc.AddNewPage();
				PdfDictionary acroForm = new PdfDictionary();
				acroForm.Put(PdfName.NeedAppearances, new PdfBoolean(true));
				doc.GetCatalog().Put(PdfName.AcroForm, acroForm);
				doc.Close();
			}
			, Throws.TypeOf<PdfAConformanceException>().With.Message.EqualTo(PdfAConformanceException.NeedAppearancesFlagOfTheInteractiveFormDictionaryShallEitherNotBePresentedOrShallBeFalse));
;
		}

		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="iTextSharp.Kernel.Xmp.XmpException"/>
		/// <exception cref="System.Exception"/>
		[Test]
		public virtual void AcroFormCheck02()
		{
			String outPdf = destinationFolder + "pdfA1b_acroFormCheck02.pdf";
			String cmpPdf = cmpFolder + "cmp_pdfA1b_acroFormCheck02.pdf";
			PdfWriter writer = new PdfWriter(outPdf);
			Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode
				.Open, FileAccess.Read);
			PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
				("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
			doc.AddNewPage();
			PdfDictionary acroForm = new PdfDictionary();
			acroForm.Put(PdfName.NeedAppearances, new PdfBoolean(false));
			doc.GetCatalog().Put(PdfName.AcroForm, acroForm);
			doc.Close();
			CompareResult(outPdf, cmpPdf);
		}

		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="iTextSharp.Kernel.Xmp.XmpException"/>
		/// <exception cref="System.Exception"/>
		[Test]
		public virtual void AcroFormCheck03()
		{
			String outPdf = destinationFolder + "pdfA1b_acroFormCheck03.pdf";
			String cmpPdf = cmpFolder + "cmp_pdfA1b_acroFormCheck03.pdf";
			PdfWriter writer = new PdfWriter(outPdf);
			Stream @is = new FileStream(sourceFolder + "sRGB Color Space Profile.icm", FileMode
				.Open, FileAccess.Read);
			PdfADocument doc = new PdfADocument(writer, PdfAConformanceLevel.PDF_A_1B, new PdfOutputIntent
				("Custom", "", "http://www.color.org", "sRGB IEC61966-2.1", @is));
			doc.AddNewPage();
			PdfDictionary acroForm = new PdfDictionary();
			doc.GetCatalog().Put(PdfName.AcroForm, acroForm);
			doc.Close();
			CompareResult(outPdf, cmpPdf);
		}

		/// <exception cref="System.IO.IOException"/>
		/// <exception cref="System.Exception"/>
		private void CompareResult(String outPdf, String cmpPdf)
		{
			String result = new CompareTool().CompareByContent(outPdf, cmpPdf, destinationFolder
				, "diff_");
			if (result != null)
			{
				NUnit.Framework.Assert.Fail(result);
			}
		}
	}
}
