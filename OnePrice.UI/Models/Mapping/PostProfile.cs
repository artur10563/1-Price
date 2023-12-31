﻿using AutoMapper;
using OnePrice.UI.Models.PostDTOs;
using OnePrice.Domain.Entities;
using OnePrice.Application.Helpers;
using OnePrice.UI.Models.Home;


namespace OnePrice.UI.Models.Mapping
{

	public class PostProfile : Profile
	{
		public PostProfile()
		{

			CreateProjection<Post, PostDisplayDTO>()
			.ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.CreatedAt.Year))
			.ForMember(dest => dest.Month, opt => opt.MapFrom(src => MonthConverter.Convert(src.CreatedAt.Month)))
			.ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.CreatedAt.Day))
			.ForMember(dest => dest.IsFavorite, opt => opt.Ignore());

			CreateMap<Post, PostDisplayDTO>()
				.ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.CreatedAt.Year))
				.ForMember(dest => dest.Month, opt => opt.MapFrom(src => MonthConverter.Convert(src.CreatedAt.Month)))
				.ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.CreatedAt.Day))
				.ForMember(dest => dest.IsFavorite, opt => opt.Ignore());

			CreateMap<Post, PostAddDTO>()
			.ForMember(dest => dest.TagsId, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.TagId).ToList()))
			.ReverseMap();



			CreateMap<Post, PostEditDTO>()
				.ForMember(dest => dest.TagsId, opt => opt.MapFrom(src => src.Tags.Select(pt => pt.TagId)
				.ToList()))
				.ReverseMap()
				.ForMember(dest => dest.ImgPath, opt => opt.Ignore());

			CreateProjection<Post, HomePostDTO>().ForMember(dest => dest.IsFavorite, opt => opt.Ignore());

			CreateMap<Post, FullPostDTO>()
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
