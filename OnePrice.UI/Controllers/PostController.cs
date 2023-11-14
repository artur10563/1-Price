using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Domain.Enums;
using OnePrice.Infrastructure.Data;
using OnePrice.UI.HelpersExtensions;
using OnePrice.UI.Models.CommonIdDTOs;
using OnePrice.UI.Models.PostDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using OnePrice.UI.Extensions;

namespace OnePrice.UI.Controllers
{
	public class PostController : Controller
	{

		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;
		public PostController(IUnitOfWork uow, IMapper mapper, AppDbContext context, IWebHostEnvironment hostingEnvironment)
		{
			_uow = uow;
			_mapper = mapper;
			_context = context;
			_hostingEnvironment = hostingEnvironment;
		}

		private async Task GetAvailableTags(IPostViewModel model) 
		{
			model.AvailableTags = await
				_uow.Tags.GetAll()
				.AsQueryable()
				.ProjectTo<CommonIdTagDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}
		private async Task GetAvailableCategories(IPostViewModel model)
		{
			model.AvailableCategories = await
				_uow.Categories.GetAll()
				.AsQueryable()
				.ProjectTo<CommonIdCategoryDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}
		private async Task GetAvailableCurrencies(IPostViewModel model)
		{
			model.AvalaibleCurrencies = await
				_uow.Currencies.GetAll()
				.AsQueryable()
				.ProjectTo<CommonIdCurrencyDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Add()
		{
			PostAddViewModel model = new PostAddViewModel();

			await GetAvailableTags(model);
			await GetAvailableCategories(model);
			await GetAvailableCurrencies(model);

			var errorMessage = TempData["ErrorMessage"] as string;
			if (!string.IsNullOrEmpty(errorMessage))
			{
				ModelState.AddModelError("", errorMessage);
			}
			if (TempData.ContainsKey("AddStatus"))
			{
				ViewData["AddStatus"] = TempData["AddStatus"];
			}

			return View(model);
		}

		[HttpPost]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> Add(PostAddViewModel postViewModel, IFormFile? image)
		{

			if (!ModelState.IsValid)
			{
				await GetAvailableTags(postViewModel);
				await GetAvailableCategories(postViewModel);
				await GetAvailableCurrencies(postViewModel);
				return View(postViewModel);
			}


			PostAddDTO post = postViewModel.Post;

			Post newPost = _mapper.Map<Post>(post);

			newPost.Tags = await
				_uow.Tags.GetAll()
				.AsQueryable()
				.Where(tag => post.TagsId.Contains(tag.Id))
				.Select(t => new PostTag { Post = newPost, Tag = t })
				.ToListAsync();

			var author = await _uow.Users.GetByEmailAsync(User.FindFirst("email").Value);
			newPost.AuthorId = author.Id;

			if (image != null)
			{

				var imageStatus = IFormFileHelper.ValidateImage(image);
				switch (imageStatus)
				{
					case ImageStatus.SizeError:
						{
							TempData["ErrorMessage"] = ("Image size must be 5 megabytes or less");
							return RedirectToAction("Add");
						}
					case ImageStatus.ExtensionError:
						{
							TempData["ErrorMessage"] = ("Only JPG or PNG files are allowed");
							return RedirectToAction("Add");
						}
					case ImageStatus.OtherError:
					default:
						{
							TempData["ErrorMessage"] = ("An unexpected error occurred. Please try again later");
							return RedirectToAction("Add");
						}
					case ImageStatus.Success:
						{
							string relativePath = "img/category";

							//need to change to  E-SHOP_\\E-SHOP.Infrastructure\\img/category
							//E-SHOP_\\E-SHOP.UI\\wwwroot + img/category
							string savePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
							string fileName = await IFormFileHelper.SaveAsync(image, Guid.NewGuid().ToString(), savePath);
							newPost.ImgPath = "/" + relativePath + "/" + fileName;
							break;
						}
				}
			}

			await _uow.Posts.AddAsync(newPost);
			await _uow.SaveChangesAsync();


			TempData["AddStatus"] = "Post added successfully";
			return RedirectToAction("Add");
		}

		[HttpGet]
		public async Task<IActionResult> Posts(
			string? category,
			string? search,
			int page = 1,
			int pageSize = 5)
		{

			var posts =
				 _context.Posts
				.Where(p => p.IsActive == true)
				.Where(p => p.Category.Name.CompareTo(category) == 0 || String.IsNullOrWhiteSpace(category))
				.Where(p =>
						String.IsNullOrWhiteSpace(search) || (
						p.Title.ToLower().Contains(search.ToLower())) ||
						p.Description.ToLower().Contains(search.ToLower()) ||
						p.Id.ToString() == search
						)
				.ProjectTo<PostDisplayDTO>(_mapper.ConfigurationProvider);



			if (page < 1) page = 1;
			if (pageSize < 5) pageSize = 5;
			if (pageSize > 50) pageSize = 50;

			ViewData["Category"] = category;
			ViewData["Search"] = search;
			return View(await posts.ToPagedListAsync(page, pageSize));
		}


		[HttpGet]
		public async Task<IActionResult> Post(int id)
		{
			FullPostDTO post = _mapper.Map<Post, FullPostDTO>(await _uow.Posts.GetByIdFullAsync(id));
			if (post == null) return NotFound();
			// post.IsActive || ( User.InRole("admin") || owner of post) ? return View() : else return NotFound(); 
			return View(post);

		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			//PostTag
			Post post = await _uow.Posts.GetByIdWithTagsAsync(id);
			if (post == null) return NotFound();


			PostEditViewModel model = new PostEditViewModel();

			await GetAvailableTags(model);
			await GetAvailableCategories(model);
			await GetAvailableCurrencies(model);

			model.Post = _mapper.Map<PostEditDTO>(post);

			if (TempData.ContainsKey("EditStatus"))
			{
				ViewData["EditStatus"] = TempData["EditStatus"];
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(PostEditViewModel postViewModel, IFormFile? image)
		{
			if (!ModelState.IsValid)
			{
				await GetAvailableTags(postViewModel);
				await GetAvailableCategories(postViewModel);
				await GetAvailableCurrencies(postViewModel);

				return View(postViewModel);
			}

			var editedPost = postViewModel.Post;
			var toEdit = await _uow.Posts.GetByIdAsync(editedPost.Id);

			if (toEdit == null) { return NotFound(); }

			_mapper.Map(editedPost, toEdit);

			//need to replace with uow
			_context.PostTags.RemoveRange(
				_context.PostTags
				.Where(pt => pt.PostId == toEdit.Id)
				.ToList());

			//Throws error with ToListAsync()
			toEdit.Tags =
				editedPost.TagsId
				.AsQueryable()
				.Select(tagId => new PostTag { PostId = toEdit.Id, TagId = tagId })
				.ToList();



			if (image != null)
			{

				var imageStatus = IFormFileHelper.ValidateImage(image);
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
							string name = "";
							if (!string.IsNullOrEmpty(toEdit.ImgPath))
							{
								name = Path.GetFileNameWithoutExtension(toEdit.ImgPath.Replace("/" + relativePath, ""));
							}
							else
							{
								name = Guid.NewGuid().ToString();
							}

							//need to change to  E-SHOP_\\E-SHOP.Infrastructure\\img/category
							//E-SHOP_\\E-SHOP.UI\\wwwroot + img/category

							string fileName = await IFormFileHelper.SaveAsync(image, name, savePath);
							toEdit.ImgPath = "/" + relativePath + "/" + fileName;
							break;
						}
				}
			}




			_uow.Posts.Update(toEdit);
			await _uow.SaveChangesAsync();

			TempData["EditStatus"] = "Post edited successfully";

			return RedirectToAction("Edit", toEdit.Id);
		}


		//[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var toDelete = await _uow.Posts
			   .GetByIdAsync(id);

			if (toDelete == null) return NotFound();

			_uow.Posts.Remove(toDelete);
			await _uow.SaveChangesAsync();


			//redirect to profile/posts later
			return RedirectToAction("Index", "Home");
		}



	}
}
