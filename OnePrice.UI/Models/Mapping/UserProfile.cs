using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.UI.Models.CommonDTOs;
using OnePrice.UI.Models.ProfileDTOs;

namespace OnePrice.UI.Models.Mapping
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<AppUser, CommonAppUserDTO>().ReverseMap();
			CreateMap<AppUser, ProfileEditDTO>()
				.ReverseMap()
				.ForMember(dest => dest.ImgPath, opt => opt.Ignore());

			CreateMap<AppUser, ProfileIndexDTO>().ReverseMap();
		}
	}
}
