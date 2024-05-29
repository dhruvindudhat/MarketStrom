using MarketStorm.DataModels.Models;
using MarketStorm.Report.Reports;
using MarketStorm.Report.Services;
using MarketStrom.DataModels.DTO;
using PdfSharp.Fonts;
using System.Diagnostics;

namespace MarketStorm.Report
{
    public class ReportController
    {
        public ReportController()
        {
            if (GlobalFontSettings.FontResolver is not FileFontResolver)
                GlobalFontSettings.FontResolver = new FileFontResolver();
        }

        public void GenerateSellBill(List<OrderDTO> soldOrders, SellBillInformation sellBillInfo)
        {
            PDFReportService pdfReport = new PDFReportService();
            string tempfile = Path.GetTempFileName();
            File.Move(tempfile, Path.ChangeExtension(tempfile, "pdf"));
            tempfile = Path.ChangeExtension(tempfile, "pdf");
            SellBill demo = new SellBill() { SoldOrders = soldOrders, SellBillInformation = sellBillInfo };
            pdfReport.InsertPage(demo);
            pdfReport.GenerateAndSave(tempfile);
            ProcessStart(tempfile);
        }

        private void ProcessStart(string Filename)
        {
            var process = new Process();
            process.StartInfo = new ProcessStartInfo(Filename)
            {
                UseShellExecute = true
            };
            process.Start();
        }
    }
}
