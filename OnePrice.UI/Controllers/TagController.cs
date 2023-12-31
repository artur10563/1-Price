﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.CommonIdDTOs;
using X.PagedList;

namespace OnePrice.UI.Controllers
{
	[Authorize(Policy = "admin")]
	[ServiceFilter(typeof(EnsureUserExistsAttribute))]
	public class TagController : Controller
	{

		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public TagController(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index(
					int page = 1,
					int pageSize = 5)
		{
			var tags = _uow.Tags
				.GetAll()
				.AsQueryable()
				.ProjectTo<CommonIdTagDTO>(_mapper.ConfigurationProvider);

			if (page < 1) page = 1;
			if (pageSize < 5) pageSize = 5;
			if (pageSize > 50) pageSize = 50;

			var paged = await tags.ToPagedListAsync(page, pageSize);

			return View(paged);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CommonIdTagDTO newTag)
		{
			if (!ModelState.IsValid) return BadRequest();

			var tag = new Tag { Name = newTag.Name };
			await _uow.Tags.AddAsync(tag);
			await _uow.SaveChangesAsync();
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Edit(CommonIdTagDTO edited)
		{
			if (!ModelState.IsValid) return BadRequest();

			var toEdit = await _uow.Tags.GetByIdAsync(edited.Id);

			if (toEdit == null) return BadRequest();

			toEdit.Name = edited.Name;

			_uow.Tags.Update(toEdit);
			await _uow.SaveChangesAsync();
			return Ok();

		}

		public async Task<IActionResult> Delete(int id)
		{
			if (!ModelState.IsValid) return BadRequest();

			var toDelete = await _uow.Tags.GetByIdAsync(id);

			if (toDelete == null) return BadRequest();

			_uow.Tags.Remove(toDelete);
			await _uow.SaveChangesAsync();
			return Ok();
		}
	}
}
