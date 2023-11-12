using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.UI.Models.CommonDTOs;

namespace OnePrice.UI.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public ProfileController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Index()
		{
			var emailClaim = User.FindFirst("email");
			var userEmail = emailClaim.Value;

			CommonAppUserDTO userDTO = null;
			var user = await _uow.Users.GetByEmailAsync(userEmail);
			if (user != null)
				userDTO = _mapper.Map<CommonAppUserDTO>(user);

			return View(userDTO);

		}
	}
}
