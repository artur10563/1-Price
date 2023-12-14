using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.CommonIdDTOs;
using X.PagedList;

namespace OnePrice.UI.Controllers
{
	[Authorize(Policy = "admin")]
	[ServiceFilter(typeof(EnsureUserExistsAttribute))]
	public class CurrencyController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public CurrencyController(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(int page = 1,
					int pageSize = 5)
		{
			var currencies = _uow.Currencies
				?.GetAll()
				?.AsQueryable()
				.ProjectTo<CommonIdCurrencyDTO>(_mapper.ConfigurationProvider);

			if (page < 1) page = 1;
			if (pageSize < 5) pageSize = 5;
			if (pageSize > 50) pageSize = 50;

			var paged = await currencies.ToPagedListAsync(page, pageSize);

			return View(paged);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CommonIdCurrencyDTO newCurrency)
		{
			if (!ModelState.IsValid) return BadRequest();

			var currency = new Currency
			{
				FullName = newCurrency.FullName,
				Code = newCurrency.Code,
				Symbol = newCurrency.Symbol
			};

			await _uow.Currencies.AddAsync(currency);
			await _uow.SaveChangesAsync();
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(CommonIdCurrencyDTO edited)
		{
			if (!ModelState.IsValid) return BadRequest();

			var toEdit = await _uow.Currencies.GetByIdAsync(edited.Id);

			if (toEdit == null) return BadRequest();

			toEdit.FullName = edited.FullName;
			toEdit.Code = edited.Code;
			toEdit.Symbol = edited.Symbol;

			_uow.Currencies.Update(toEdit);
			await _uow.SaveChangesAsync();
			return Ok();

		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			if (!ModelState.IsValid) return BadRequest();

			var toDelete = await _uow.Currencies.GetByIdAsync(id);

			if (toDelete == null) return BadRequest();

			_uow.Currencies.Remove(toDelete);
			await _uow.SaveChangesAsync();
			return Ok();
		}


	}
}
