using AutoMapper;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI
{
	public class MappingConfig : Profile
	{
		public MappingConfig()
		{
            CreateMap<Models.Villa, VillaDTO>();
            CreateMap<VillaDTO, Models.Villa>();

            CreateMap<Models.Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Models.Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<Models.VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<Models.VillaNumber, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<Models.VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
		}
	}
}
