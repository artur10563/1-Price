using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.CommonDTOs;
using OnePrice.UI.Models.ProfileDTOs;

namespace OnePrice.UI.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public ProfileController(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		[HttpGet]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Edit()
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailAsync(email);
			var userDTO = _mapper.Map<ProfileEditDTO>(user);
			return View(userDTO);
		}

		[HttpPost]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Edit(ProfileEditDTO edited, IFormFile image)
		{
			if(!ModelState.IsValid) return View();

			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailAsync(email);

			_mapper.Map(edited, user);
			//Restore email and created date
			return Ok(user);
			_uow.Users.Update(user);
			await _uow.SaveChangesAsync();

			return RedirectToAction(nameof(Edit));
		}
	}
}
