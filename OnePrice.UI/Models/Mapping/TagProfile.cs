using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.UI.Models.CommonDTOs;
using OnePrice.UI.Models.CommonIdDTOs;

namespace OnePrice.UI.Models.Mapping
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
