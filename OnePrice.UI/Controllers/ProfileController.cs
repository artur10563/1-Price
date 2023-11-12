using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.CommonDTOs;

namespace OnePrice.UI.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public ProfileController(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper  = mapper;
		}

		[HttpGet]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Index()
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailAsync(email);
			var userDTO = _mapper.Map<CommonAppUserDTO>(user);
			return View(userDTO);

		}
	}
}
