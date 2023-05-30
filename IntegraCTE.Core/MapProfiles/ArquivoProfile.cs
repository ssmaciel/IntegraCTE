using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Services.Model;
using IntegraCTE.Core.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.MapProfiles
{
    public class ArquivoProfile : Profile
    {
        public ArquivoProfile()
        {
            CreateMap<ArquivoDTO, ArquivoModel>().ReverseMap();
            CreateMap<CTE, ArquivoModel>().ReverseMap();
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
