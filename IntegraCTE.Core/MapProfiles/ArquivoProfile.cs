﻿using AutoMapper;
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
        }
    }
}
