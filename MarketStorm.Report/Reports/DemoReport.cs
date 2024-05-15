using MarketStorm.Report.Services;
using MarketStrom.DataModels.DTO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Text.RegularExpressions;

namespace MarketStorm.Report.Reports
{
    public class DemoReport : ReportTemplate, IPDFPage
    {
        public override IPDFPage CreateContent(Document document)
        {
            Section section = document.LastSection;
            section.PageSetup.LeftMargin = "0.5cm";
            section.PageSetup.RightMargin = "0.5cm";
            section.PageSetup.TopMargin = "0.5cm";
            section.PageSetup.BottomMargin = "0cm";
            base.CreateContent(document);

            //section.AddParagraph("Sales Bill", ReportStyleNames.Title);

            Table headerTable = section.AddTable();
            headerTable.TopPadding = 2;
            headerTable.BottomPadding = 2;
            headerTable.Borders.Visible = true;
            headerTable.Borders.Color = Colors.Black;

            //ADD COLUMNS
            headerTable.AddColumn(Unit.FromCentimeter(1.8));
            headerTable.AddColumn(Unit.FromCentimeter(6));
            headerTable.AddColumn(Unit.FromCentimeter(2));
            headerTable.AddColumn(Unit.FromCentimeter(2));
            headerTable.AddColumn(Unit.FromCentimeter(2));
            headerTable.AddColumn(Unit.FromCentimeter(2));
            headerTable.AddColumn(Unit.FromCentimeter(2));
            headerTable.AddColumn(Unit.FromCentimeter(2.5));

            //HEADING DETAILS
            Row row = headerTable.AddRow();
            row.Cells[7].MergeDown = 2;
            Cell cell = row.Cells[0];
            cell.MergeRight = 6;
            cell.AddParagraph("RAMDEV TRADING CO");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.Format.Font.Size = 12;
            cell.Format.Font.Bold = true;
            cell.Style = ReportStyleNames.Title;
            cell.Borders.Bottom.Visible = false;

            cell = row.Cells[7];
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.VerticalAlignment = VerticalAlignment.Center;
            cell.AddParagraph(Regex.Replace(DateTime.Today.ToString("dd-MM-yyyy") + DateTime.Today.ToString("dddd"), ".{10}", "$0 "));

            row = headerTable.AddRow();
            cell = row.Cells[0];
            cell.MergeRight = 6;
            cell.AddParagraph(" SHOP NO G 1 MARKETING YARD HAPA JAMNAGAR");
            cell.Format.Alignment = ParagraphAlignment.Center;
            cell.Borders.Bottom.Visible = false;

            row = headerTable.AddRow();
            cell = row.Cells[0];
            cell.MergeRight = 6;
            cell.AddParagraph(" MO 9723489567 / 9714000260 / 9913166453");
            cell.Format.Alignment = ParagraphAlignment.Center;

            //ADD DETAILED ORDERS HEADERS
            row = headerTable.AddRow();
            row.Cells[0].AddParagraph("Sr. No.");
            row.Cells[0].Style = ReportStyleNames.CenterBold;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[1].AddParagraph("Item");
            row.Cells[1].Style = ReportStyleNames.CenterBold;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[2].AddParagraph("Qty.");
            row.Cells[2].Style = ReportStyleNames.CenterBold;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[3].AddParagraph("Weight");
            row.Cells[3].Style = ReportStyleNames.CenterBold;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[4].AddParagraph("Price");
            row.Cells[4].Style = ReportStyleNames.CenterBold;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[5].AddParagraph("Commission");
            row.Cells[5].Style = ReportStyleNames.CenterBold;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[6].AddParagraph("Labour");
            row.Cells[6].Style = ReportStyleNames.CenterBold;
            row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
            row.Cells[7].AddParagraph("Total");
            row.Cells[7].Style = ReportStyleNames.CenterBold;
            row.Cells[7].VerticalAlignment = VerticalAlignment.Center;

            //ADD DETAILED DATA
            if (SoldOrders.Count > 0)
            {
                int srno = 1;
                int totalqty = 0;
                double totalweight = 0;
                double totalamount = 0;
                double totalcommission = 0;
                double totallabour = 0;
                foreach (OrderDTO soldOrder in SoldOrders)
                {
                    row = headerTable.AddRow();
                    row.Borders.Bottom.Visible = SoldOrders.Count == srno ? true : false;
                    row.Cells[0].AddParagraph(srno.ToString());
                    row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                    // row.Cells[0].Borders.Bottom.Visible = false;
                    row.Cells[1].AddParagraph(soldOrder.CategoryName);
                    row.Cells[1].Style = ReportStyleNames.Bold;
                    row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                    //row.Cells[1].Borders.Bottom.Visible = false;
                    row.Cells[2].AddParagraph(soldOrder.Quantity != null ? soldOrder.Quantity.Value.ToString() : "-");
                    row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                    //row.Cells[2].Borders.Bottom.Visible = false;
                    row.Cells[3].AddParagraph(soldOrder.Kg != null ? soldOrder.Kg.Value.ToString() : "-");
                    row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                    //row.Cells[3].Borders.Bottom.Visible = false;
                    row.Cells[4].AddParagraph(soldOrder.Price.ToString());
                    row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
                    //row.Cells[4].Borders.Bottom.Visible = false;
                    row.Cells[5].AddParagraph(soldOrder.Comission != null ? soldOrder.Comission.Value.ToString() : "-");
                    row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                    row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                    //row.Cells[5].Borders.Bottom.Visible = false;
                    row.Cells[6].AddParagraph(soldOrder.LabourAmount != null ? soldOrder.LabourAmount.Value.ToString() : "-");
                    row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
                    row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                    //row.Cells[6].Borders.Bottom.Visible = false;
                    row.Cells[7].AddParagraph(soldOrder.TotalAmount.ToString());
                    row.Cells[7].Format.Alignment = ParagraphAlignment.Right;
                    row.Cells[7].VerticalAlignment = VerticalAlignment.Center;
                    //row.Cells[7].Borders.Bottom.Visible = false;

                    srno++;
                    totalqty += soldOrder.Quantity != null ? soldOrder.Quantity.Value : 0;
                    totalweight += soldOrder.Kg != null ? soldOrder.Kg.Value : 0;
                    totalamount += soldOrder.TotalAmount;
                    totalcommission += soldOrder.ComissionAmount != null ? soldOrder.ComissionAmount.Value : 0;
                    totallabour += soldOrder.LabourAmount != null ? soldOrder.LabourAmount.Value : 0;
                }
                row = headerTable.AddRow();
                row.Cells[1].AddParagraph("Total");
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[2].AddParagraph(totalqty.ToString());
                row.Cells[2].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[3].AddParagraph(totalweight.ToString());
                row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[5].AddParagraph(totalcommission.ToString());
                row.Cells[5].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[5].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[6].AddParagraph(totallabour.ToString());
                row.Cells[6].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[6].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[7].AddParagraph(totalamount.ToString());
                row.Cells[7].VerticalAlignment = VerticalAlignment.Center;
                row.Cells[7].Format.Alignment = ParagraphAlignment.Right;
                // row.Cells[7].Borders.Top.Visible = true;
            }
            else
            {
                row = headerTable.AddRow();
                cell = row.Cells[0];
                cell.MergeRight = 7;
                cell.Format.Alignment = ParagraphAlignment.Center;
                cell.AddParagraph("There Is No Order To Attach!!");
            }


            return this;
        }

        public List<OrderDTO> SoldOrders { get; set; }
    }
}
