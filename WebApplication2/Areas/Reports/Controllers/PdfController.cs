using iText.Barcodes;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Areas.Reports.Controllers
{
    [Area("Reports")]
    public class PdfController : WebApplication2.Controllers.BaseController<PdfController>
    {
        public IActionResult Index(bool nolayout = false)
        {
            if (nolayout) ViewBag.NoLayout = true;
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public async Task<IActionResult> Test(int id, string L)
        {
            var list = new List<byte[]>();
            list.Add(await TestDocument());
            byte[] byte1 =  Combine(list);
            return File(byte1, "application/pdf", $"Test.pdf");
        }
        public async Task<byte[]> TestDocument()
        {
            Image img = new Image(ImageDataFactory.Create(hostingEnvironment.WebRootPath + "/img/cconx.png")).SetTextAlignment(TextAlignment.CENTER).SetWidth(60);
            Image logo = new Image(ImageDataFactory.Create(hostingEnvironment.WebRootPath + "/img/cconx.png")).SetTextAlignment(TextAlignment.CENTER).SetWidth(150);
            var header = new header { Title = "Hello", description = "D", subdescription = "S", date = DateTime.Now.ToShortDateString(), time = DateTime.Now.ToShortTimeString(), project = "P", docno = "d", qrcode = "QR", logo = logo, img = img };
            var table = new Table(4, false);
            var pagesize = PageSize.A4.Rotate();
            return await AddDocument(header, table, pagesize, new float[] { 120f, 10f, 10f, 10f });
        }
        public class header
        {
            public string Title { get; set; }
            public Image img { get; set; }
            public Image logo { get; set; }
            public string description { get; set; }
            public string subdescription { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public string docno { get; set; }
            public string project { get; set; }
            public string qrcode { get; set; }
        }
        public async Task<byte[]> AddDocument(header header, Table table, PageSize pagesize, float[] margins)
        {
            byte[] buffer;
            PdfDocument pdfDoc = null;
            using (MemoryStream memStream = new MemoryStream())
            {
                using (PdfWriter pdfWriter = new PdfWriter(memStream))
                {
                    pdfWriter.SetCloseStream(true);
                    using (pdfDoc = new PdfDocument(pdfWriter))
                    {
                        using (Document document = new Document(pdfDoc))
                        {
                            VariableHeaderEventHandler handler = new VariableHeaderEventHandler();
                            document.SetMargins(margins[0], margins[1], margins[2], margins[3]);
                            pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, handler);
                            pdfDoc.SetDefaultPageSize(pagesize);
                            pdfDoc.SetCloseWriter(true);
                            pdfDoc.SetCloseReader(true);
                            pdfDoc.SetFlushUnusedObjects(true);
                            document.SetFontSize(10f);
                            handler.SetHeader(pdfDoc, document, header.img, header.logo, header.description, header.subdescription, header.date, header.time, header.docno, header.project, header.qrcode, margins);
                            document.Add(table);
                        }
                    }
                }
                buffer = memStream.ToArray();
            }
            return buffer;
        }
        private class VariableHeaderEventHandler : IEventHandler
        {
            protected Document doc;
            protected PdfDocument pdf;
            protected Image img;
            protected Image logo;
            protected string headerdescription;
            protected string subheaderdescription;
            protected string headerdate;
            protected string headertime;
            protected string documentno;
            protected string project;
            protected string barcode;
            protected Table table2;
            protected float[] margins;

            public void SetHeader(PdfDocument _pdf, Document _document, Image _img, Image _logo, string _headerdescription, string? _subheaderdescription, string _headerdate, string _headertime, string _documentno, string? _project, string _barcode, float[] _margins)
            {
                this.doc = _document;
                this.pdf = _pdf;
                this.headerdescription = _headerdescription;
                this.subheaderdescription = _subheaderdescription;
                this.headerdate = _headerdate;
                this.headertime = _headertime;
                this.documentno = _documentno;
                this.project = _project;
                this.barcode = _barcode;
                this.img = _img;
                this.logo = _logo;
                this.margins = _margins;
            }

            public void HandleEvent(Event currentEvent)
            {
                PdfDocumentEvent documentEvent = (PdfDocumentEvent)currentEvent;
                PdfPage page = documentEvent.GetPage();


                var qbar = new BarcodeQRCode();
                qbar.SetCode(barcode);
                var qbarcodeImg = new Image(qbar.CreateFormXObject(pdf));
                //qbarcodeImg.SetHeight(100f); ;
                //qbarcodeImg.SetWidth(100f);
                qbarcodeImg.SetPaddings(0, 0, 0, 0);
                table2 = new Table(5, false);
                table2.UseAllAvailableWidth();

                table2.AddCell(new Cell(5, 1).SetPadding(0).SetWidth(90f).SetMaxWidth(90f).Add(qbarcodeImg.SetWidth(90f)));
                table2.AddCell(new Cell(4, 1).SetPadding(0).Add(logo.SetPadding(0).SetWidth(200f).SetMaxHeight(80f)));
                table2.AddCell(new Cell(4, 1).Add(new Paragraph("" + headerdescription).SetTextAlignment(TextAlignment.CENTER).SetFontSize(20)).Add(new Paragraph("" + subheaderdescription).SetTextAlignment(TextAlignment.CENTER)));
                table2.AddCell(new Cell(1, 1).SetMaxWidth(30f).Add(new Paragraph("Date").SetFontSize(8)));
                table2.AddCell(new Cell(1, 1).SetMaxWidth(60f).Add(new Paragraph("" + headerdate).SetFontSize(8)));
                table2.AddCell(new Cell(1, 1).SetMaxWidth(30f).Add(new Paragraph("Time").SetFontSize(8)));
                table2.AddCell(new Cell(1, 1).Add(new Paragraph("" + headertime).SetFontSize(8)));
                table2.AddCell(new Cell(1, 1).SetMaxWidth(30f).Add(new Paragraph("Doc").SetFontSize(8)));
                table2.AddCell(new Cell(1, 1).Add(new Paragraph("" + documentno).SetFontSize(8)));
                table2.AddCell(new Cell(1, 1).SetMaxWidth(30f).Add(new Paragraph("Page").SetFontSize(8)));
                table2.AddCell(new Cell(1, 1).Add(new Paragraph("" + pdf.GetPageNumber(documentEvent.GetPage()))));
                table2.AddCell(new Cell(1, 1).Add(img.SetWidth(30f)));
                table2.AddCell(new Cell(1, 3).Add(new Paragraph("" + project)));

                table2.SetMargins(10f, margins[1], 0, margins[3]);
                //PdfCanvas canvas = new PdfCanvas(page);
                //AffineTransform transformationMatrix = AffineTransform.GetScaleInstance(
                //    page.GetPageSize().GetWidth() ,
                //    page.GetPageSize().GetHeight());
                //canvas.ConcatMatrix(transformationMatrix); 
                new Canvas(page, page.GetPageSize()).Add(table2).Close();
            }
        }
        public byte[] Combine(IEnumerable<byte[]> pdfs)
        {
            using (var writerMemoryStream = new MemoryStream())
            {
                using (var writer = new PdfWriter(writerMemoryStream))
                {
                    using (var mergedDocument = new PdfDocument(writer))
                    {
                        var merger = new PdfMerger(mergedDocument);

                        foreach (var pdfBytes in pdfs)
                        {
                            using (var copyFromMemoryStream = new MemoryStream(pdfBytes))
                            {
                                using (var reader = new PdfReader(copyFromMemoryStream))
                                {
                                    using (var copyFromDocument = new PdfDocument(reader))
                                    {
                                        merger.Merge(copyFromDocument, 1, copyFromDocument.GetNumberOfPages());
                                    }
                                }
                            }
                        }
                    }
                }
                return writerMemoryStream.ToArray();
            }
        }
    }
}
