using AutoMapper;
using E_SHOP.UI.Models.PostDTOs;
using E_SHOP.Domain.Entities;

namespace E_SHOP.UI.Models.Mapping
{
	public class PostProfile : Profile
	{
		public PostProfile() 
		{
			CreateMap<Post, PostDisplayDTO>();
			CreateProjection<Post, PostDisplayDTO>();
		}
	}
}
