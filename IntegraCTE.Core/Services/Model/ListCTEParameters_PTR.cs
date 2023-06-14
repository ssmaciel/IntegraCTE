using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services.Model
{
    public class ListCTEParameters_PTR
    {
        public string odatacontext { get; set; }
        public CTEParameters_PTR[] value { get; set; }
    }

    public class CTEParameters_PTR
    {
        public string DataArea { get; set; }

        public string AddressEmail { get; set; }

        public string Password { get; set; }

        public string OperationTypeID { get; set; }

        public string ItemId { get; set; }

        public string Model { get; set; }
    }
}
