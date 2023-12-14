using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Domain.Enums;
using OnePrice.Infrastructure.Data;
using OnePrice.UI.HelpersExtensions;
using OnePrice.UI.Models.PostDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;
using OnePrice.UI.Extensions;
using OnePrice.UI.Helpers;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Hosting;


namespace OnePrice.UI.Controllers
{
	public class PostController : Controller
	{

		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _hostingEnvironment;
		private readonly AvailableDataService _availableDataService;
		private readonly IHtmlLocalizer<SharedResource> _localizer;
		public PostController(IUnitOfWork uow, IMapper mapper, AppDbContext context, IWebHostEnvironment hostingEnvironment, AvailableDataService availableDataService, IHtmlLocalizer<SharedResource> localizer)
		{
			_uow = uow;
			_mapper = mapper;
			_context = context;
			_hostingEnvironment = hostingEnvironment;
			_availableDataService = availableDataService;
			_localizer = localizer;
		}

		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		[HttpGet]
		public async Task<IActionResult> Add()
		{
			PostAddViewModel model = new PostAddViewModel();

			model.AvailableTags = _availableDataService.GetAvailableTags().ToList();
			model.AvailableCategories = _availableDataService.GetAvailableCategories().ToList();
			model.AvailableCurrencies = _availableDataService.GetAvailableCurrencies().ToList();

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
				postViewModel.AvailableTags = _availableDataService.GetAvailableTags().ToList();
				postViewModel.AvailableCategories = _availableDataService.GetAvailableCategories().ToList();
				postViewModel.AvailableCurrencies = _availableDataService.GetAvailableCurrencies().ToList();
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
							string relativePath = "img/post";

							string savePath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
							string fileName = await IFormFileHelper.SaveAsync(image, Guid.NewGuid().ToString(), savePath);
							newPost.ImgPath = "/" + relativePath + "/" + fileName;
							break;
						}
				}
			}

			await _uow.Posts.AddAsync(newPost);
			await _uow.SaveChangesAsync();


			TempData["AddStatus"] = _localizer["PostAddSuccess"].Value;
			return RedirectToAction("Add");
		}

		[HttpGet]
		public async Task<IActionResult> Posts(
			string search,
			string category,
			string currency,
			string sortOrder,
			string sortField,
			int? minPrice,
			int? maxPrice,
			int page = 1,
			int pageSize = 5)
		{

			if (page < 1) page = 1;
			if (pageSize < 5) pageSize = 5;
			if (pageSize > 50) pageSize = 50;


			var posts = await _uow.Posts.GetFiltered(search, category, currency, minPrice, maxPrice, sortField, sortOrder)
				.AsQueryable()
				.ProjectTo<PostDisplayDTO>(_mapper.ConfigurationProvider)
				.ToPagedListAsync(page, pageSize);

			if (User.Identity.IsAuthenticated)
			{
				var email = User.FindFirst("email").Value;
				var user = await _uow.Users.GetByEmailWithPostsAsync(email);

				foreach (var postDto in posts)
				{
					postDto.IsFavorite = user.FavoritePosts.Any(fp => fp.PostId == postDto.Id && fp.UserId == user.Id);
				}
			}


			var model = new SearchPostDisplayViewModel()
			{
				Posts = posts,
				Filters = new()
				{
					AvailableCategories = _availableDataService.GetAvailableCategories().ToList(),
					AvailableCurrencies = _availableDataService.GetAvailableCurrencies().ToList(),
					Search = search,
					Category = category,
					Currency = currency,
					MaxPrice = maxPrice,
					MinPrice = minPrice,
					SortField = sortField,
					SortOrder = sortOrder
				}
			};

			return View(model);

		}


		[HttpGet]
		public async Task<IActionResult> Post(int id)
		{
			FullPostDTO post = _mapper.Map<Post, FullPostDTO>(await _uow.Posts.GetByIdFullAsync(id));
			if (post == null) return NotFound();
			// post.IsActive || ( User.InRole("admin") || owner of post) ? return View() : else return NotFound(); 
			return View(post);

		}

		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			//PostTag
			Post post = await _uow.Posts.GetByIdWithTagsAsync(id);
			if (post == null) return NotFound();

			var email = User.FindFirst("email").Value;
			var isAdmin = User.FindFirst("role")?.Value == "admin";
			var user = await _uow.Users.GetByEmailAsync(email);

			if (!isAdmin)
				if (post.Author.Id != user.Id )
				{
					TempData["ErrorMessage"] = _localizer["AccessDenied"].Value;
					return RedirectToAction("Index", "Profile");
				}

			var model = new PostEditViewModel()
			{
				AvailableTags = _availableDataService.GetAvailableTags().ToList(),
				AvailableCategories = _availableDataService.GetAvailableCategories().ToList(),
				AvailableCurrencies = _availableDataService.GetAvailableCurrencies().ToList(),
				Post = _mapper.Map<PostEditDTO>(post)
			};


			if (TempData.ContainsKey("EditStatus"))
			{
				ViewData["EditStatus"] = TempData["EditStatus"];
			}
			return View(model);
		}

		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		[HttpPost]
		public async Task<IActionResult> Edit(PostEditViewModel postViewModel, IFormFile? image)
		{
			if (!ModelState.IsValid)
			{

				postViewModel.AvailableTags = _availableDataService.GetAvailableTags().ToList();
				postViewModel.AvailableCategories = _availableDataService.GetAvailableCategories().ToList();
				postViewModel.AvailableCurrencies = _availableDataService.GetAvailableCurrencies().ToList();

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
							string relativePath = "img/post";
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

			TempData["EditStatus"] = _localizer["PostEditSuccess"].Value;

			return RedirectToAction("Edit", toEdit.Id);
		}

		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var toDelete = await _uow.Posts
			   .GetByIdCommentsTags(id);

			if (toDelete == null) return NotFound();

			var email = User.FindFirst("email").Value;
			var isAdmin = User.FindFirst("role")?.Value == "admin";
			var user = await _uow.Users.GetByEmailAsync(email);

			if (toDelete.Author.Id == user.Id || isAdmin)
			{
				_uow.Posts.Remove(toDelete);
				await _uow.SaveChangesAsync();

				TempData["SuccessMessage"] = _localizer["PostDeleteSuccess"].Value;
			}
			else
			{
				TempData["ErrorMessage"] = _localizer["AccessDenied"].Value;
			}

			return Ok();
		}

		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		[HttpGet]
		public async Task<IActionResult> UserFilteredPosts(bool isActive)
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithPostsAsync(email);

			var posts = user.Posts
				.Where(p => p.IsActive == isActive)
				.AsQueryable()
				.ProjectTo<PostDisplayDTO>(_mapper.ConfigurationProvider)
				.ToList();

			posts.ForEach(postDto => postDto.IsFavorite = user.FavoritePosts.Any(fp => fp.PostId == postDto.Id && fp.UserId == user.Id));


			return PartialView("_LongPostListPartial", posts);
		}

		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		[HttpGet]
		public async Task<IActionResult> GetFavorite()
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithPostsAsync(email);

			var posts = user.FavoritePosts
				.Select(fp => fp.Post)
				.AsQueryable()
				.ProjectTo<PostDisplayDTO>(_mapper.ConfigurationProvider)
				.ToList();

			posts.ForEach(post => post.IsFavorite = true);

			return PartialView("_LongPostListPartial", posts);
		}


		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		[HttpPost]
		public async Task<IActionResult> AddFavorite(int postId)
		{

			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithPostsAsync(email);

			var existingFp = user.FavoritePosts.FirstOrDefault(p => p.PostId == postId);

			if (existingFp != null)
			{
				user.FavoritePosts.Remove(existingFp);
			}
			else
			{
				user.FavoritePosts.Add(new FavoriteUserPost()
				{
					PostId = postId,
					UserId = user.Id
				});
			}


			await _uow.SaveChangesAsync();
			return Ok();
		}
	}
}
