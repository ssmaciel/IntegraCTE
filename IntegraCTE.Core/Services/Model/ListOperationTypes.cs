using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services.Model
{
    public class ListOperationTypes
    {
        public string odatacontext { get; set; }
        public OperationTypes[] value { get; set; }
    }

    public class OperationTypes
    {
        public string odataetag { get; set; }
        public string dataAreaId { get; set; }
        public string OperationTypeID { get; set; }
        public string CreateInventTrans { get; set; }
        public string CustPostingProfile { get; set; }
        public string VendPostingProfile { get; set; }
        public string CreateFinancialTrans { get; set; }
        public string LedgerDimensionDisplayValue { get; set; }
        public string Name { get; set; }
        public long RecId_PTR { get; set; }
    }
}
