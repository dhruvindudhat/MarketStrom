using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;

namespace MarketStorm.Report.Services
{
    public class PDFReportService
    {

        private List<IPDFPage> _reportContents = new List<IPDFPage>();

        public bool GenerateAndSave(string filename)
        {
            Document document = CreateDocument();
            DefineStyles(document);

            foreach (IPDFPage report in _reportContents)
            {
                AddDocumentSection(ref document);
                report.CreateContent(document);
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = document
            };

            renderer.RenderDocument();

            try
            {
                renderer.PdfDocument.Save(filename);
            }
            catch { return false; }

            return true;
        }

        private Document CreateDocument()
        {
            try
            {
                Document document = new Document();
                document.Info.Title = "TestPro Report";
                document.Info.Subject = "TestPro Report";
                document.Info.Author = "TestPro";
                return document;
            }
            catch (Exception e)
            {
                return new Document();
            }
        }

        public PDFReportService InsertPage(IPDFPage content)
        {
            _reportContents.Add(content);

            return this;
        }

        private void AddDocumentSection(ref Document document)
        {
            PageSetup p = document.DefaultPageSetup.Clone();
            p.HeaderDistance = 20;
            p.FooterDistance = 20;
            p.TopMargin = 80;
            p.BottomMargin = 40;
            p.LeftMargin = 20;
            p.RightMargin = 20;
            Section section = document.AddSection();

            section.PageSetup = p;
        }

        private PDFReportService DefineStyles(Document document)
        {
            Style style = document.Styles[StyleNames.Normal];
            
            //style.Font.Name = "Arial";
            style.Font.Size = 8;

            style = document.Styles.AddStyle(ReportStyleNames.GreenNormal, StyleNames.Normal);
            style.Font.Color = Color.Parse("green");

            style = document.Styles.AddStyle(ReportStyleNames.RedNormal, StyleNames.Normal);
            style.Font.Color = Color.Parse("red");

            style = document.Styles.AddStyle(ReportStyleNames.GreenRightNormal, ReportStyleNames.GreenNormal);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;

            style = document.Styles.AddStyle(ReportStyleNames.RedRightNormal, ReportStyleNames.RedNormal);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;

            style = document.Styles.AddStyle(ReportStyleNames.GreenCenterNormal, ReportStyleNames.GreenNormal);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles.AddStyle(ReportStyleNames.RedCenterNormal, ReportStyleNames.RedNormal);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles.AddStyle(ReportStyleNames.RedLeftNormal, ReportStyleNames.RedNormal);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Left;

            style = document.Styles.AddStyle(ReportStyleNames.RedLeftBold, ReportStyleNames.RedLeftNormal);
            style.Font.Bold = true;

            style = document.Styles.AddStyle(ReportStyleNames.Bold, StyleNames.Normal);
            style.Font.Bold = true;

            style = document.Styles.AddStyle(ReportStyleNames.CenterBold, ReportStyleNames.Bold);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles.AddStyle(ReportStyleNames.Italic, StyleNames.Normal);
            style.Font.Italic = true;

            style = document.Styles.AddStyle(ReportStyleNames.Title, StyleNames.Normal);
            style.Font.Size = document.Styles[StyleNames.Normal].Font.Size + 10;
            style.Font.Bold = true;
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles.AddStyle(ReportStyleNames.Subtitle, ReportStyleNames.Title);
            style.Font.Size = document.Styles[StyleNames.Normal].Font.Size + 4;

            style = document.Styles.AddStyle(ReportStyleNames.CenterNormal, StyleNames.Normal);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Center;

            style = document.Styles.AddStyle(ReportStyleNames.RightNormal, StyleNames.Normal);
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;

            style = document.Styles.AddStyle(ReportStyleNames.RightBold, ReportStyleNames.RightNormal);
            style.Font.Bold = true;

            style = document.Styles[StyleNames.Heading1];
            style.Font.Size = document.Styles[StyleNames.Normal].Font.Size + 4;
            style.Font.Bold = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles[StyleNames.Heading2];
            style.Font.Size = document.Styles[StyleNames.Normal].Font.Size + 2;
            style.Font.Bold = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles[StyleNames.Heading3];
            style.Font.Size = document.Styles[StyleNames.Normal].Font.Size + 2;
            style.Font.Bold = true;
            style.Font.Italic = true;
            style.ParagraphFormat.SpaceAfter = 6;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;

            // Create a new style called TextBox based on style Normal
            style = document.Styles.AddStyle("TextBox", "Normal");
            style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            style.ParagraphFormat.Borders.Width = 2.5;
            style.ParagraphFormat.Borders.Distance = "3pt";
            style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            // Create a new style called TOC based on style Normal
            style = document.Styles.AddStyle(ReportStyleNames.TOC, StyleNames.Normal);
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Blue;

            return this;
        }
    }
}
