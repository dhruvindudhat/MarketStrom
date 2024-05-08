using PdfSharp.Fonts;

namespace MarketStorm.Report
{
    public class FileFontResolver : IFontResolver // FontResolverBase
    {
        public string DefaultFontName => "Arial";

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                using (var fs = File.OpenRead(faceName))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            string filePath = string.Empty;

            if (isBold && isItalic)
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "arialbi.ttf");
            else if (isBold)
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "arialbd.ttf");
            else if (isItalic)
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "ariali.ttf");
            else
                filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "arial.ttf");

            if (File.Exists(filePath))
                return new FontResolverInfo(filePath);
            else
                return null;
        }
    }

}
