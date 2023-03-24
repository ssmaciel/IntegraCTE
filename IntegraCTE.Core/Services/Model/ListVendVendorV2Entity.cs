using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services.Model
{
    public class ListVendVendorV2Entity
    {
        public string odatacontext { get; set; }
        public VendVendorV2Entity[] value { get; set; }
    }

    public class VendVendorV2Entity
    {
        public string BrazilianCNPJOrCPF { get; set; }

        public string VendorOrganizationName { get; set; }

        public string VendorAccountNumber { get; set; }
    }

}
