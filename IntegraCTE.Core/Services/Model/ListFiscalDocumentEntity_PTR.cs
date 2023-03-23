using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services.Model
{
    public class ListFiscalDocumentEntity_PTR
    {
        public string odatacontext { get; set; }
        public FiscalDocumentEntity_PTR[] value { get; set; }
    }

    public partial class FiscalDocumentEntity_PTR
    {
        public string odataetag { get; set; }
        public string dataAreaId { get; set; }
        public string Submission { get; set; }
        public string SubmitReturnResponse { get; set; }
        public string Voucher { get; set; }
        public string Status { get; set; }
        public string SalesId { get; set; }
        public string AccessKey { get; set; }
        public string FiscalDocumentNumber { get; set; }
        public string FiscalDocumentSeries { get; set; }
        public DateTime CreatedDateTime_PTR { get; set; }
        public string FiscalEstablishment { get; set; }
    }
}
