using DinkToPdf.Contracts;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tubaina.Encoder.Model;

namespace Tubaina.Encoder
{
    public static class ServiceHandler
    {
        public static void ServeToDisk(byte[] bytes, string path)
        {
            using var writer = new BinaryWriter(File.OpenWrite(path));
            writer.Write(bytes);
        }

        public static Service CreateHandle(ServiceRequest request, IConverter converter, string webRoot)
        {
            var settings = new DefaultSettings(request, webRoot);
            var document = settings.GenerateHtmlToPdf();
            var file = converter.Convert(document);

            ServeToDisk(file, @"C:\Users\davia\Desktop\test.pdf");

            return new Service()
            {
                Uri = request.Identification,
                File = "created"
            };
        }
    }
}
