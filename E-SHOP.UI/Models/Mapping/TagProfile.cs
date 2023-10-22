using AutoMapper;
using E_SHOP.Domain.Entities;
using E_SHOP.UI.Models.CommonDTOs;
using E_SHOP.UI.Models.CommonIdDTOs;

namespace E_SHOP.UI.Models.Mapping
{
    public class TagProfile : Profile
	{
		public TagProfile()
		{
			CreateMap<Tag, CommonIdTagDTO>().ReverseMap();
			CreateProjection<Tag, CommonIdTagDTO>();

			CreateMap<Tag, CommonTagDTO>();
			//CreateProjection<Tag,CommonTagDTO>();
		}
	}
}
