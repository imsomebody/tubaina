using DinkToPdf;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tubaina.Encoder.HTML;
using Tubaina.Encoder.Model;

namespace Tubaina.Encoder
{
    internal class DefaultSettings
    {
        public readonly string TEMPLATE_FILE;

        public readonly GlobalSettings globals = new GlobalSettings();
        public readonly ObjectSettings objectSettings = new ObjectSettings();
        public readonly WebSettings webSettings = new WebSettings();
        public readonly FooterSettings footerSettings = new FooterSettings();

        private readonly string applicationRoot;
        private readonly ServiceRequest originRequest;

        /// <summary>
        /// Cria uma instância das configurações padrão de exportação de Guia de Serviço.
        /// </summary>
        /// <param name="originRequest"></param>
        public DefaultSettings(ServiceRequest originRequest, string webRoot)
        {
            this.originRequest = originRequest;
            this.applicationRoot = webRoot;

            // Set Template
            this.TEMPLATE_FILE = Path.Combine(Current.AssemblyDirectory, "HTML", "Etiqueta1.html");

            this.SetPageConfiguration();
            this.LoadHtmlPage();
            this.SetPageFooter();
        }

        /// <summary>
        /// Atualiza as <see cref="objectSettings"/> com o footer e a página.
        /// </summary>
        private void UpdateObject()
        {
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
        }

        /// <summary>
        /// Configura a impressão.
        /// </summary>
        private void SetPageConfiguration()
        {
            globals.ColorMode = ColorMode.Color;
            globals.Orientation = Orientation.Portrait;
            globals.PaperSize = new PechkinPaperSize("100mm", "180mm");
            globals.Margins = new MarginSettings { Top = 15, Bottom = 15 };
            // PagesCount = false causes DivideByZeroException
            objectSettings.PagesCount = true;
        }

        /// <summary>
        /// Configura o Footer da página.
        /// </summary>
        private void SetPageFooter()
        {
            footerSettings.FontSize = 8;
            footerSettings.Center = "Autenticação\n" + Crypt.CreateSha256(DateTime.Now.ToString()).Substring(0, 12);
            footerSettings.Line = true;
        }

        /// <summary>
        /// Carrega o template HTML para o objeto.
        /// </summary>
        private void LoadHtmlPage()
        {
            objectSettings.HtmlContent = Replacer.Replace(new (string, string)[] {
                ("imageBase64", QR.GetBase64($"teste 123 -> user: {originRequest.Identification}")),
                ("moment", DateTime.Now.ToString()),
                ("operator", originRequest.Identification),
                ("descriptionShort", $"{(originRequest.Information.Length > 400 ? originRequest.Information.Substring(0, 399) : originRequest.Information )}...")
            }, File.ReadAllText(TEMPLATE_FILE));
            webSettings.DefaultEncoding = "utf-8";
        }

        /// <summary>
        /// Retorna o Documento pronto para ser convertido baseado nas configurações atuais.
        /// </summary>
        /// <returns></returns>
        public HtmlToPdfDocument GenerateHtmlToPdf()
        {
            this.UpdateObject();

            return new HtmlToPdfDocument()
            {
                GlobalSettings = globals,
                Objects = { objectSettings }
            };
        }
    }
}
