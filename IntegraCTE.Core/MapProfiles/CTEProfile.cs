using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Services;
using IntegraCTE.Core.Services.Model;
using IntegraCTE.Core.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.MapProfiles
{
    public class CTEProfile : Profile
    {
        public CTEProfile()
        {
            CreateMap<CTE, CTEModel>()
                .ForMember(s => s.DestinatarioCNPJCPF, opt => opt.MapFrom(a => a.Destinatario.CNPJCPF))
                .ForMember(s => s.DestinatarioNome, opt => opt.MapFrom(a => a.Destinatario.Nome))
                .ForMember(s => s.DestinatarioLogradouro, opt => opt.MapFrom(a => a.Destinatario.Logradouro))
                .ForMember(s => s.DestinatarioNro, opt => opt.MapFrom(a => a.Destinatario.Nro))
                .ForMember(s => s.DestinatarioBairro, opt => opt.MapFrom(a => a.Destinatario.Bairro))
                .ForMember(s => s.DestinatarioCodigoMunicipio, opt => opt.MapFrom(a => a.Destinatario.CodigoMunicipio))
                .ForMember(s => s.DestinatarioMunicipio, opt => opt.MapFrom(a => a.Destinatario.Municipio))
                .ForMember(s => s.DestinatarioCEP, opt => opt.MapFrom(a => a.Destinatario.CEP))
                .ForMember(s => s.DestinatarioUF, opt => opt.MapFrom(a => a.Destinatario.UF))
                .ForMember(s => s.DestinatarioCodigoPais, opt => opt.MapFrom(a => a.Destinatario.CodigoPais))
                .ForMember(s => s.DestinatarioPais, opt => opt.MapFrom(a => a.Destinatario.Pais))
                .ForMember(s => s.Notas, opt => opt.MapFrom(a => a.NotaFiscal))
                .ReverseMap();

            CreateMap<CTEModel, CTERequest>()
                .ConstructUsing(s => new CTERequest("", s.ChaveAcessoCte, s.DataEmissao, s.ModeloCte, s.NumeroCte, s.SerieCte, s.Justificativa, s.NotaFiscal))
                .ForPath(s => s.Linha.DestinatarioNome, opt => opt.MapFrom(a => a.DestinatarioNome))
                .ForPath(s => s.Linha.DestinatarioLogradouro, opt => opt.MapFrom(a => a.DestinatarioLogradouro))
                .ForPath(s => s.Linha.DestinatarioNro, opt => opt.MapFrom(a => a.DestinatarioNro))
                .ForPath(s => s.Linha.DestinatarioBairro, opt => opt.MapFrom(a => a.DestinatarioBairro))
                .ForPath(s => s.Linha.DestinatarioMunicipio, opt => opt.MapFrom(a => a.DestinatarioMunicipio))
                .ForPath(s => s.Linha.DestinatarioCEP, opt => opt.MapFrom(a => a.DestinatarioCEP))
                .ForPath(s => s.Linha.DestinatarioUF, opt => opt.MapFrom(a => a.DestinatarioUF))
                .ForPath(s => s.Linha.DestinatarioCodigoPais, opt => opt.MapFrom(a => a.DestinatarioCodigoPais))
                .ForPath(s => s.OrderVendorAccountNumber, opt => opt.MapFrom(a => a.Transportadora.CodigoExterno))
                .ForPath(s => s.VendorPaymentMethodName, opt => opt.MapFrom(a => a.Transportadora.MetodoPagamento))
                .ForPath(s => s.VendorPaymentMethodSpecificationName, opt => opt.MapFrom(a => a.Transportadora.EspecificacaoMetodoPagamento))
                .ReverseMap();

            CreateMap<NotaDTO, FiscalDocumentEntity_PTR>()
                .ForMember(s => s.AccessKey, opt => opt.MapFrom(a => a.ChaveNotaFical))
                .ForMember(s => s.FiscalDocumentNumber, opt => opt.MapFrom(a => a.NumeroNotaFical))
                .ForMember(s => s.FiscalDocumentSeries, opt => opt.MapFrom(a => a.SerieNotaFical))
                .ForMember(s => s.FiscalEstablishment, opt => opt.MapFrom(a => a.Estabelecimento))
                .ReverseMap();

            CreateMap<Transportadora, TransportadoraModel>().ReverseMap();
            CreateMap<TransportadoraDTO, TransportadoraModel>().ReverseMap();
            CreateMap<TransportadoraResponse, TransportadoraDTO>().ReverseMap();
            CreateMap<TransportadoraResponse, TransportadoraModel>().ReverseMap();
        }
    }
}
