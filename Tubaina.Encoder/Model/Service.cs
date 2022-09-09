using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tubaina.Encoder.Model
{
    public class ServiceRequest
    {
        public string Identification { get; set; }
        public string Information { get; set; }
        public string File { get; set; }
    }

    public class Service
    {
        public string Uri { get; set; }
        public object File { get; set; }
    }
}
