using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.UI.Models.CommentDTOs;

namespace OnePrice.UI.Models.Mapping
{
	public class CommentProfile : Profile
	{
		public CommentProfile()
		{
			CreateMap<Comment, CommentDisplayDTO>().ReverseMap();

			CreateMap<Comment, CommentAddDTO>().ReverseMap();
		}
	}
}
