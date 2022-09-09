using DinkToPdf.Contracts;

using Microsoft.AspNetCore.Mvc;

using Tubaina.Encoder;
using Tubaina.Encoder.Model;

namespace Tubaina.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IConverter _converter;
        private readonly IWebHostEnvironment _environment;

        public ServiceController(ILogger<ServiceController> logger, IWebHostEnvironment environ,IConverter converter)
        {
            this._logger = logger;
            this._converter = converter;
            this._environment = environ;
        }

        [HttpPost("pdf")]
        public Service GetPdf(ServiceRequest request)
        {
            return ServiceHandler.CreateHandle(request, this._converter, this._environment.WebRootPath != null ? this._environment.WebRootPath : this._environment.ContentRootPath);
        }
    }
}
