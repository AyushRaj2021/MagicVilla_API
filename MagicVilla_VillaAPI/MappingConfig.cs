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
            CreateMap<Models.VillaNumberDTO, VillaDTO>();
            CreateMap<VillaDTO, Models.VillaNumberDTO>();

            CreateMap<Models.VillaNumberDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<Models.VillaNumberDTO, VillaUpdateDTO>().ReverseMap();
		}
	}
}
