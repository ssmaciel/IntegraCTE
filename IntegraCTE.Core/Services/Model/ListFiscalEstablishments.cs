using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.Services.Model
{

    public class ListFiscalEstablishments
    {
        public string odatacontext { get; set; }
        public FiscalEstablishments[] value { get; set; }
    }

    public class FiscalEstablishments
    {
        public string odataetag { get; set; }
        public string dataAreaId { get; set; }
        public string FiscalEstablishmentId { get; set; }
        public string NFeDigitalCertificate { get; set; }
        public string FciToIntrastateEnabled { get; set; }
        public string AccountantCPF { get; set; }
        public string AddressName { get; set; }
        public string CSCToken { get; set; }
        public string MatchNfeXmlOnPosting { get; set; }
        public string NFCeAuthority { get; set; }
        public int NextFiscalPrinterDailyReportNumber { get; set; }
        public string NFeVersion { get; set; }
        public string ClosedWarehouse { get; set; }
        public bool ValidateXmlSchema { get; set; }
        public string NIRE { get; set; }
        public string AccountantCRC { get; set; }
        public string PreprintedSecurityForm { get; set; }
        public string FiscalEstablishmentGroupId { get; set; }
        public string NFCeEnvironment { get; set; }
        public string EmailTemplateCorrectionLetterNFe { get; set; }
        public string EFDocNFe402018005v110 { get; set; }
        public object NFCeVersion { get; set; }
        public string PartyNumber { get; set; }
        public string NFeAuthority { get; set; }
        public string BlockPostingNotValidatedNfeXml { get; set; }
        public string NFCeSendPdfInEmail { get; set; }
        public string CNPJ { get; set; }
        public string NFeEnvironment { get; set; }
        public string CCM { get; set; }
        public string Name { get; set; }
        public bool PrintDanfeWhenAproved { get; set; }
        public string SPEDContribReportingType { get; set; }
        public string CSC { get; set; }
        public string EmailTemplateCanceledNFe { get; set; }
        public string EmailTemplateApprovedNFe { get; set; }
        public string FiscalEstablishmentSalesIssuerId { get; set; }
        public string SendDanfePdfInEmail { get; set; }
        public string IE { get; set; }
        public string EFDocNFeTechNotes { get; set; }
        public string NFCeEmailApproved { get; set; }
    }
}
