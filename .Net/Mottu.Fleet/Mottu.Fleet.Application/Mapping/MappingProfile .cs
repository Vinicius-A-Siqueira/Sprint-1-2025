using AutoMapper;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Usuário
        CreateMap<UsuarioMongo, UserDto>();
        CreateMap<CreateUserDto, UsuarioMongo>();
        CreateMap<UpdateUserDto, UsuarioMongo>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Pátio
        CreateMap<PatioMongo, PatioDto>()
            .ForMember(dest => dest.QuantidadeMotos, opt => opt.Ignore())
            .ForMember(dest => dest.TaxaOcupacao, opt => opt.Ignore());

        CreateMap<CreatePatioDto, PatioMongo>();
        CreateMap<UpdatePatioDto, PatioMongo>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Moto
        CreateMap<MotoMongo, MotoDto>()
            .ForMember(dest => dest.StatusDescricao, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<CreateMotoDto, MotoMongo>();
        CreateMap<UpdateMotoDto, MotoMongo>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
