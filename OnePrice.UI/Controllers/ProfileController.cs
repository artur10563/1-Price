using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Domain.Enums;
using OnePrice.UI.Extensions;
using OnePrice.UI.HelpersExtensions;
using OnePrice.UI.Models.ProfileDTOs;

namespace OnePrice.UI.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public ProfileController(IUnitOfWork uow, IMapper mapper, IWebHostEnvironment hostingEnvironment)
		{
			_uow = uow;
			_mapper = mapper;
			_hostingEnvironment = hostingEnvironment;
		}

		#region Edit

		[HttpGet]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Edit()
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailAsync(email);
			var userDTO = _mapper.Map<ProfileEditDTO>(user);

			if (TempData["EditStatus"] != null)
			{
				ViewData["EditStatus"] = TempData["EditStatus"] as string;
			}
			return View(userDTO);
		}

		[HttpPost]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Edit(ProfileEditDTO edited, IFormFile? image)
		{

			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailAsync(email);

			if (!ModelState.IsValid)
			{
				var userDTO = _mapper.Map<ProfileEditDTO>(user);
				return View(userDTO);
			}
			_mapper.Map(edited, user);
			//Dont allow user to change email
			user.Email = email;


			if (image != null)
			{

				var imageStatus = IFormFileHelper.ValidateImage(image);
				switch (imageStatus)
				{
					case ImageStatus.SizeError:
						{
							ModelState.AddModelError("ImgPath", "Image size must be 5 megabytes or less");
							return View();
						}
					case ImageStatus.ExtensionError:
						{
							ModelState.AddModelError("ImgPath", "Only JPG or PNG files are allowed");
							return View();
						}
					case ImageStatus.OtherError:
					default:
						{
							ModelState.AddModelError("ImgPath", "An unexpected error occurred. Please try again later");
							return View();
						}
					case ImageStatus.Success:
						{
							string relativePath = "img/profile";
							string savePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
							string name;
							if (!string.IsNullOrEmpty(user.ImgPath))
							{
								name = Path.GetFileNameWithoutExtension(user.ImgPath.Replace("/" + relativePath, ""));
							}
							else
							{
								name = Guid.NewGuid().ToString();
							}

							string fileName = await IFormFileHelper.SaveAsync(image, name, savePath);
							user.ImgPath = "/" + relativePath + "/" + fileName;
							break;
						}
				}
			}


			_uow.Users.Update(user);
			var updateResult = await _uow.SaveChangesAsync();

			if (updateResult != 0) TempData["EditStatus"] = "Updated succesfully"; ;

			return RedirectToAction(nameof(Edit));
		}

		#endregion

		[HttpGet]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Index()
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithPostsAsync(email);
			var userDTO = _mapper.Map<AppUser, ProfileIndexDTO>(user);
			return View(userDTO);
		}
	}
}
