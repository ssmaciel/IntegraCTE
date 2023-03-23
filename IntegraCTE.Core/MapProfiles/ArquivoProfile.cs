using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Entity;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Services.Model;
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
            CreateMap<CTE, CTEModel>().ReverseMap();
            CreateMap<NotaDTO, FiscalDocumentEntity_PTR>()
                .ForMember(s => s.AccessKey, opt => opt.MapFrom(a => a.ChaveNotaFical))
                .ForMember(s => s.FiscalDocumentNumber, opt => opt.MapFrom(a => a.NumeroNotaFical))
                .ForMember(s => s.FiscalDocumentSeries, opt => opt.MapFrom(a => a.SerieNotaFical))
                .ForMember(s => s.FiscalEstablishment, opt => opt.MapFrom(a => a.Estabelecimento))
                .ReverseMap();
        }
    }
}
