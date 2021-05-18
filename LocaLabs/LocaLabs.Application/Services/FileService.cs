using DinkToPdf;

namespace LocaLabs.Application.Services
{
    public interface IFileService
    {
        byte[] AsPdf(string content);
    }

    public class FileService : IFileService
    {
        public byte[] AsPdf(string htmlContent)
        {
            var converter = new SynchronizedConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    PaperSize = PaperKind.A4Plus,
                    Orientation = Orientation.Landscape,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            return converter.Convert(doc);
        }
    }
}