using AutoMapper;
using OnePrice.Domain.Entities;
using OnePrice.UI.Models.CommonDTOs;
using OnePrice.UI.Models.CommonIdDTOs;

namespace OnePrice.UI.Models.Mapping
{
	public class CurrencyProfile : Profile
	{
		public CurrencyProfile()
		{
			CreateProjection<Currency, CommonIdCurrencyDTO>();
			CreateMap<Currency, CommonCurrencyDTO>();
		}
	}
}
