using AutoMapper;
using E_SHOP.UI.Models.PostDTOs;
using E_SHOP.Domain.Entities;
using E_SHOP.Application.Helpers;
using E_SHOP.UI.Models.Home;
using E_SHOP.UI.Models.CommonDTOs;

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

			CreateMap<Post, FullPostDTO>()


			   //.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(tag => new CommonTagDTO
			   //{
			   //	Name = tag.Tag.Name
			   //}).ToList()))
			  .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.Tag)))
			  .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.CreatedAt.Year))
			  .ForMember(dest => dest.Month, opt => opt.MapFrom(src => MonthConverter.Convert(src.CreatedAt.Month)))
			  .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.CreatedAt.Day))
			  .ForMember(dest => dest.Minute, opt => opt.MapFrom(src => src.CreatedAt.Minute))
			  .ForMember(dest => dest.Hour, opt => opt.MapFrom(src => src.CreatedAt.Hour))
			  ;






		}
	}
}
