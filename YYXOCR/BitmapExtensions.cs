using System.Drawing;

namespace YYXOCR
{
    public static class BitmapExtensions
    {
        public static Bitmap ToBlackAndWhite(this Bitmap bitmap)
        {
            var cloneBitmap = new Bitmap(bitmap);

            for (var i = 0; i < cloneBitmap.Width; i++)
            {
                for (var j = 0; j < cloneBitmap.Height; j++)
                {
                    var pixel = cloneBitmap.GetPixel(i, j);
                    const int colorValue = 666;
                    var result = pixel.R * +pixel.G + pixel.B > colorValue ? 255 : 0;
                    var color = Color.FromArgb(1, result, result, result);
                    cloneBitmap.SetPixel(i, j, color);
                }
            }

            return cloneBitmap;
        }
    }
}
