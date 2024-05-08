using MigraDoc.DocumentObjectModel;

namespace MarketStorm.Report.Services
{
    public interface IPDFPage
    {
        IPDFPage CreateContent(Document document);
    }
}
