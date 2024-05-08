using MarketStorm.Report.Services;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace MarketStorm.Report.Reports
{
    public class DemoReport : ReportTemplate, IPDFPage
    {
        public override IPDFPage CreateContent(Document document)
        {
            Section section = document.LastSection;
            section.PageSetup.LeftMargin = "1cm";
            section.PageSetup.RightMargin = "1cm";
            section.PageSetup.TopMargin = "1cm";
            section.PageSetup.BottomMargin = "1cm";
            base.CreateContent(document);

            //section.AddParagraph("Sales Bill", ReportStyleNames.Title);

            Table headerTable = section.Headers.Primary.AddTable();
            headerTable.AddColumn(Unit.FromCentimeter(18));

            Row row = headerTable.AddRow();
            Cell cell = row.Cells[0];
            cell.AddParagraph("Company Name");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Format.Font.Size = 12;
            cell.Format.Font.Bold = true;

            row = headerTable.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph("Address Line 1");
            cell.Format.Alignment = ParagraphAlignment.Center;

            row = headerTable.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph("Address Line 2");
            cell.Format.Alignment = ParagraphAlignment.Center;

            row = headerTable.AddRow();
            cell = row.Cells[0];
            cell.AddParagraph("City, State, Zip");
            cell.Format.Alignment = ParagraphAlignment.Center;

            // Add a border around the header table
            headerTable.Borders.Width = 0.5;
            headerTable.Borders.Color = Colors.Black;            

            // Add content to the document (e.g., invoice details)
            
            //// Header
            //var headerTable = section.AddTable();
            //headerTable.Borders.Visible = true;
            //headerTable.Style = "Normal";
            //headerTable.AddColumn(Unit.FromCentimeter(10));
            //headerTable.AddColumn(Unit.FromCentimeter(6));
            //headerTable.AddRow();

            //var cell = headerTable[0, 0];
            //cell.AddParagraph("JAY SHREE KHODIYAR").Format.Font.Bold = true;
            //cell.VerticalAlignment = VerticalAlignment.Center;

            //cell = headerTable[0, 1];
            //cell.AddParagraph("JAY RAMDEVPIR").Format.Font.Bold = true;
            //cell.VerticalAlignment = VerticalAlignment.Center;

            //headerTable.AddRow();

            //cell = headerTable[1, 0];
            //cell.AddParagraph($"GSTIN:GSTIN12343445");
            //cell.VerticalAlignment = VerticalAlignment.Center;

            //cell = headerTable[1, 1];
            //cell.AddParagraph("Address");
            //cell.VerticalAlignment = VerticalAlignment.Center;

            //headerTable.AddRow();

            //cell = headerTable[2, 0];
            //cell.AddParagraph("+91 7575088440");
            //cell.VerticalAlignment = VerticalAlignment.Center;

            //// Bill Details
            //var table = section.AddTable();
            //table.Style = "Normal";
            //table.Borders.Color = Color.Empty;
            //table.AddColumn(Unit.FromCentimeter(1.5)); // SR
            //table.AddColumn(Unit.FromCentimeter(4.5));  // Item Name
            //table.AddColumn(Unit.FromCentimeter(1.5)); // HSN
            //table.AddColumn(Unit.FromCentimeter(1));  // Bags
            //table.AddColumn(Unit.FromCentimeter(2));  // Weight
            //table.AddColumn(Unit.FromCentimeter(2));  // Rate
            //table.AddColumn(Unit.FromCentimeter(2.5));  // Amount

            //var headerRow = table.AddRow();
            //headerRow.Format.Font.Bold = true;
            //headerRow.Cells[0].AddParagraph("Sr");
            //headerRow.Cells[1].AddParagraph("Item Name");
            //headerRow.Cells[2].AddParagraph("H.S.N.");
            //headerRow.Cells[3].AddParagraph("Bags");
            //headerRow.Cells[4].AddParagraph("Weight");
            //headerRow.Cells[5].AddParagraph("Rate");
            //headerRow.Cells[6].AddParagraph("Amount");

            //// Bill Items
            //var tableRaw = table.AddRow();
            //tableRaw.Cells[0].AddParagraph("1");
            //tableRaw.Cells[1].AddParagraph("ONION");
            //tableRaw.Cells[2].AddParagraph("071220");
            //tableRaw.Cells[3].AddParagraph("8.0");
            //tableRaw.Cells[4].AddParagraph("330.50");
            //tableRaw.Cells[5].AddParagraph("400");
            //tableRaw.Cells[6].AddParagraph("6610");

            return this;
        }
    }
}
