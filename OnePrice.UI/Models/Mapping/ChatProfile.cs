
using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.Domain.Entities.ChatEntities;
using OnePrice.UI.Models.ChatDTOs;

namespace OnePrice.UI.Models.Mapping
{
	public class ChatProfile : Profile
	{
		public ChatProfile()
		{

			CreateMap<Message, ChatMessageDTO>();
			CreateMap<AppUser, ChatUserDTO>();
			CreateProjection<UserChat, ChatDTO>()
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Chat.Title));
			CreateMap<Chat, ChatDTO>().ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.Id));
		}
	}
}
