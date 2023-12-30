using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.UI.Models.ComplaintDTOs;

namespace OnePrice.UI.Models.Mapping
{
	public class ComplaintProfile : Profile
	{
		public ComplaintProfile()
		{
			CreateMap<ComplaintType, ComplaintTypeDTO>().ReverseMap();
			CreateMap<Complaint, ComplaintAddDTO>().ReverseMap();
			CreateMap<Complaint, ComplaintStatusDTO>().ReverseMap();
		}
	}
}
