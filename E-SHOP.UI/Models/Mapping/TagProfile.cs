using AutoMapper;
using E_SHOP.Domain.Entities;
using E_SHOP.UI.Models.PostDTOs;

namespace E_SHOP.UI.Models.Mapping
{
	public class TagProfile : Profile
	{
		public TagProfile()
		{
			CreateMap<Tag, TagDTO>().ReverseMap();
			CreateProjection<Tag, TagDTO>();
		}
	}
}
