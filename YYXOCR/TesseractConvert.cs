using System.Drawing;
using Tesseract;

namespace YYXOCR
{
    public static class TesseractConvert
    {
        public static string ToText(Bitmap bitmap)
        {
            using (var engine = new TesseractEngine(@"./tessdata", "chi_sim", EngineMode.Default))
            {
                var page = engine.Process(bitmap);
                var text = page.GetText();
                return text;
            }
        }
    }
}
