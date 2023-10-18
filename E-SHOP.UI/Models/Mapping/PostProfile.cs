using AutoMapper;
using E_SHOP.UI.Models.PostDTOs;
using E_SHOP.Domain.Entities;
using E_SHOP.Application.Helpers;
using E_SHOP.UI.Models.Home;

namespace E_SHOP.UI.Models.Mapping
{

	public class PostProfile : Profile
	{
		public PostProfile()
		{

			CreateProjection<Post, PostDisplayDTO>()
			.ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.CreatedAt.Year))
			.ForMember(dest => dest.Month, opt => opt.MapFrom(src => MonthConverter.Convert(src.CreatedAt.Month)))
			.ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.CreatedAt.Day)); ;

			CreateMap<Post, PostDisplayDTO>()
		   .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.CreatedAt.Year))
		   .ForMember(dest => dest.Month, opt => opt.MapFrom(src => MonthConverter.Convert(src.CreatedAt.Month)))
		   .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.CreatedAt.Day));

			CreateMap<Post, PostAddDTO>()
			.ForMember(dest => dest.TagsId, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.TagId)
			.ToList()));

			CreateMap<Post, PostEditDTO>()
			.ForMember(dest => dest.TagsId, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.TagId)
			.ToList()));

			CreateProjection<Post, HomePostDTO>();







		}
	}
}
