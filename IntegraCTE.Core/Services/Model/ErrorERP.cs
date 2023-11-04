using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services.Model
{
    public class ErrorERP
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public string message { get; set; }
        public Internalexception innererror { get; set; }
    }

    public class Internalexception
    {
        public string message { get; set; }
        public string type { get; set; }
        public string stacktrace { get; set; }
        public Internalexception? internalexception { get; set; }
    }

}
