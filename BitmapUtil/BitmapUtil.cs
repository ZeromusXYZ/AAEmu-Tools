using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace BitmapUtil;
public class BitmapUtil
{
    public static Bitmap? ReadDDSFromStream(Stream stream)
    {
        using (var image = Pfim.Pfimage.FromStream(stream))
        {
            var format = image.Format switch
            {
                Pfim.ImageFormat.Rgb24 => PixelFormat.Format24bppRgb,
                Pfim.ImageFormat.Rgba32 => PixelFormat.Format32bppArgb,
                Pfim.ImageFormat.R5g5b5 => PixelFormat.Format16bppRgb555,
                Pfim.ImageFormat.R5g6b5 => PixelFormat.Format16bppRgb565,
                Pfim.ImageFormat.R5g5b5a1 => PixelFormat.Format16bppArgb1555,
                Pfim.ImageFormat.Rgb8 => PixelFormat.Format8bppIndexed,
                _ => throw new NotImplementedException()
            };

            var pointer = Marshal.UnsafeAddrOfPinnedArrayElement(image.Data, 0);
            var bitmap = new Bitmap(image.Width, image.Height, image.Stride, format, pointer);

            return bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat) as Bitmap;
        }
    }
}
