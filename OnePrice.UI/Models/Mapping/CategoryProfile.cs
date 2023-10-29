using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.UI.Models.CommonDTOs;
using OnePrice.UI.Models.CommonIdDTOs;

namespace OnePrice.UI.Models.Mapping
{
    public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, CommonIdCategoryDTO>();
			CreateProjection<Category, CommonIdCategoryDTO>();
		
			CreateMap<Category, CommonCategoryDTO>();

		
		}
	}
}
