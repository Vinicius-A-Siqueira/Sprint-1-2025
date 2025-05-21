using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Moto, MotoDTO>().ReverseMap();
            CreateMap<CreateMotoDTO, Moto>();
            CreateMap<Filial, FilialDTO>().ReverseMap();
            CreateMap<Patio, PatioDTO>().ReverseMap();
        }
    }
}