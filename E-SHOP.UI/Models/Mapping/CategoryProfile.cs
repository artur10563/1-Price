using AutoMapper;
using E_SHOP.Domain.Entities;
using E_SHOP.UI.Models.CommonDTOs;
using E_SHOP.UI.Models.CommonIdDTOs;

namespace E_SHOP.UI.Models.Mapping
{
    public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, CommonIdCategoryDTO>();
			CreateProjection<Category, CommonIdCategoryDTO>();
		
			CreateProjection<Category, CommonCategoryDTO>();
		
		}
	}
}
