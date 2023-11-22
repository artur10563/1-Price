using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Domain.Enums;
using OnePrice.UI.Extensions;
using OnePrice.UI.HelpersExtensions;
using OnePrice.UI.Models.CategoryDTOs;
using X.PagedList;

namespace OnePrice.UI.Controllers
{
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _hostingEnvironment;

		public CategoryController(IUnitOfWork uow, IMapper mapper, IWebHostEnvironment hostingEnvironment)
		{
			_uow = uow;
			_mapper = mapper;
			_hostingEnvironment = hostingEnvironment;
		}

		public async Task<IActionResult> Index(
			int page = 1,
			int pageSize = 5)
		{
			var categories = _uow.Categories
				.GetAll()
				.AsQueryable()
				.ProjectTo<FullCategoryDTO>(_mapper.ConfigurationProvider);

			if (page < 1) page = 1;
			if (pageSize < 5) pageSize = 5;
			if (pageSize > 50) pageSize = 50;

			var paged = await categories.ToPagedListAsync(page, pageSize);

			return View(paged);
		}


		[HttpPost]
		[Authorize(Policy = "admin")]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Edit(FullCategoryDTO edited, IFormFile? img)
		{
			//TODO: handle bad input
			if (!ModelState.IsValid) return BadRequest();

			var toEdit = await _uow.Categories.GetByIdAsync(edited.Id);

			if (toEdit == null) return BadRequest();

			toEdit.Name = edited.Name;

			if (img != null)
			{

				var imageStatus = IFormFileHelper.ValidateImage(img);
				switch (imageStatus)
				{
					case ImageStatus.SizeError:
						{
							ModelState.AddModelError("ImgPath", "Image size must be 10 megabytes or less");
							break;
						}
					case ImageStatus.ExtensionError:
						{
							ModelState.AddModelError("ImgPath", "Only JPG or PNG files are allowed");
							break;
						}
					case ImageStatus.OtherError:
					default:
						{
							ModelState.AddModelError("", "An unexpected error occurred. Please try again later");
							break;
						}
					case ImageStatus.Success:
						{
							string relativePath = "img/category";
							string savePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);

							string name;
							if (!string.IsNullOrEmpty(toEdit.ImgPath))
							{
								name = Path.GetFileNameWithoutExtension(toEdit.ImgPath.Replace("/" + relativePath, ""));
							}
							else
							{
								name = Guid.NewGuid().ToString();
							}


							string fileName = await IFormFileHelper.SaveAsync(img, name, savePath);
							toEdit.ImgPath = "/" + relativePath + "/" + fileName;
							break;
						}
				}
			}
			_uow.Categories.Update(toEdit);
			await _uow.SaveChangesAsync();
			return Ok();
		}

		[HttpPost]
		[Authorize(Policy = "admin")]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Add(FullCategoryDTO newCategory, IFormFile? img)
		{
			if (!ModelState.IsValid) return BadRequest();
			var category = new Category()
			{
				Name = newCategory.Name
			};

			if (img != null)
			{

				var imageStatus = IFormFileHelper.ValidateImage(img);
				switch (imageStatus)
				{
					case ImageStatus.SizeError:
						{
							TempData["ErrorMessage"] = ("Image size must be 5 megabytes or less");
							return RedirectToAction("Index");
						}
					case ImageStatus.ExtensionError:
						{
							TempData["ErrorMessage"] = ("Only JPG or PNG files are allowed");
							return RedirectToAction("Index");
						}
					case ImageStatus.OtherError:
					default:
						{
							TempData["ErrorMessage"] = ("An unexpected error occurred. Please try again later");
							return RedirectToAction("Index");
						}
					case ImageStatus.Success:
						{
							string relativePath = "img/category";

							string savePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
							string fileName = await IFormFileHelper.SaveAsync(img, Guid.NewGuid().ToString(), savePath);
							category.ImgPath = "/" + relativePath + "/" + fileName;
							break;
						}
				}
			}
			await _uow.Categories.AddAsync(category);
			await _uow.SaveChangesAsync();
			return Ok();

		}


	}
}
