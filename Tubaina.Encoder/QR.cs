using QRCoder;
using QRCoder.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubaina.Encoder
{
    /// <summary>
    /// Fornece ferramentas para a conversão de strings de dados em Códigos QR.
    /// </summary>
    internal class QR
    {
        public static string GetBase64(string data)
        {
            using(var qrGenerator = new QRCodeGenerator())
            using(var qrCode = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q))
            using(var qrBmp = new BitmapByteQRCode(qrCode))
            {
                return Convert.ToBase64String(qrBmp.GetGraphic(20));
            }
        }
    }
}
