using AutoMapper;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Usuário
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Pátio
        CreateMap<Patio, PatioDto>()
            .ForMember(dest => dest.QuantidadeMotos, opt => opt.Ignore())
            .ForMember(dest => dest.TaxaOcupacao, opt => opt.Ignore());

        CreateMap<CreatePatioDto, Patio>();
        CreateMap<UpdatePatioDto, Patio>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Moto
        CreateMap<Moto, MotoDto>()
            .ForMember(dest => dest.PatioNome, opt => opt.MapFrom(src => src.Patio.Nome))
            .ForMember(dest => dest.StatusDescricao, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<CreateMotoDto, Moto>();
        CreateMap<UpdateMotoDto, Moto>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
