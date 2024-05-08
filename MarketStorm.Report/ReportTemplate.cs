using MarketStorm.Report.Services;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Reflection;

namespace MarketStorm.Report
{
    public class ReportTemplate : IPDFPage
    {
        public virtual IPDFPage CreateContent(Document document)
        {
            DefineHeader(document.LastSection.Headers.Primary);
            DefineFooter(document.LastSection.Footers.Primary);

            return this;
        }
        protected void DefineFooter(HeaderFooter primary)
        {
            Table table = primary.AddTable();

            Column column = table.AddColumn(Unit.FromCentimeter(6.5));
            column.Format.Alignment = ParagraphAlignment.Left;

            column = table.AddColumn(Unit.FromCentimeter(6.5));
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn(Unit.FromCentimeter(6.5));
            column.Format.Alignment = ParagraphAlignment.Right;

            Row row = table.AddRow();
            row.Cells[0].AddParagraph("Version" + ": " + Assembly.GetEntryAssembly().GetName().Version.ToString());
            row.Cells[0].Style = StyleNames.Normal;
            row.Cells[1].AddParagraph("MarketStrom");
            row.Cells[1].Style = ReportStyleNames.CenterNormal;
            row.Cells[2].AddParagraph("ReportPrinted" + ": " + DateTime.Now.ToString());
            row.Cells[2].Style = ReportStyleNames.RightNormal;

        }

        protected void DefineHeader(HeaderFooter primary)
        {


        }

    }
}
