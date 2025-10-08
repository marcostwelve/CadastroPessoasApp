using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Application.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Pessoa, PessoaDto>().ReverseMap();
        CreateMap<Pessoa, PessoaUpdate>().ReverseMap();
        CreateMap<Pessoa, PessoaView>().ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome.ToUpper()))
            .ReverseMap();

        CreateMap<UserDto, ApplicationUser>().ReverseMap();
        CreateMap<Endereco, EnderecoDto>().ReverseMap();
    }
}
