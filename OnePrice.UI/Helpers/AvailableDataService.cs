using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnePrice.Application.Repository;
using OnePrice.UI.Models.CommonIdDTOs;

namespace OnePrice.UI.Helpers
{
	public class AvailableDataService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _uow;

		public AvailableDataService(IMapper mapper, IUnitOfWork uow)
		{
			_mapper = mapper;
			_uow = uow;
		}

		public IEnumerable<CommonIdTagDTO> GetAvailableTags()
		{
			return
				_uow.Tags.GetAll()
				.AsQueryable()
				.ProjectTo<CommonIdTagDTO>(_mapper.ConfigurationProvider);
		}
		public IEnumerable<CommonIdCategoryDTO> GetAvailableCategories()
		{
			return
				_uow.Categories.GetAll()
				.AsQueryable()
				.ProjectTo<CommonIdCategoryDTO>(_mapper.ConfigurationProvider);
		}
		public IEnumerable<CommonIdCurrencyDTO> GetAvailableCurrencies()
		{
			return
				_uow.Currencies.GetAll()
				.AsQueryable()
				.ProjectTo<CommonIdCurrencyDTO>(_mapper.ConfigurationProvider);
		}

	}
}
