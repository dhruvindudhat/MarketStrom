using MarketStorm.Report.Reports;
using MarketStorm.Report.Services;
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

        public void OpenPDFReport()
        {
            PDFReportService pdfReport = new PDFReportService();
            string tempfile = Path.GetTempFileName();
            File.Move(tempfile, Path.ChangeExtension(tempfile, "pdf"));
            tempfile = Path.ChangeExtension(tempfile, "pdf");
            DemoReport demo = new DemoReport();
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
