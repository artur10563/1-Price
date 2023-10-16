using AutoMapper;
using E_SHOP.Domain.Entities;
using E_SHOP.UI.Models.PostDTOs;

namespace E_SHOP.UI.Models.Mapping
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, CategoryDTO>();
			CreateProjection<Category, CategoryDTO>();
		}
	}
}
