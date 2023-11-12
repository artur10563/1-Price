using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.UI.Models.CommonDTOs;

namespace OnePrice.UI.Models.Mapping
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<AppUser, CommonAppUserDTO>().ReverseMap();
		}
	}
}
