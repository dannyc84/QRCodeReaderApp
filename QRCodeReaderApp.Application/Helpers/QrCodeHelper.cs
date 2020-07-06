using System;
using System.Linq;
using System.Text;

namespace QRCodeReaderApp.Application.Helpers
{
    public class QrCodeHelper
    {
        public static QrCodeFormat GetImageFormat(byte[] bytes)
        {
            var gif = Encoding.ASCII.GetBytes("GIF");
            var png = new byte[] { 137, 80, 78, 71 };
            var jpeg = new byte[] { 255, 216, 255, 224 };

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return QrCodeFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return QrCodeFormat.png;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return QrCodeFormat.jpeg;

            return QrCodeFormat.unknown;
        }
    }
}
