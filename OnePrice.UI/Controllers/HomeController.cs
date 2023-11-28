using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnePrice.Application.Repository;
using OnePrice.UI.Models;
using OnePrice.UI.Models.CommonDTOs;
using OnePrice.UI.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using OnePrice.UI.Helpers;

namespace OnePrice.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly AvailableDataService _availableDataService;

		public HomeController(
			IUnitOfWork uow,
			IMapper mapper,
			AvailableDataService availableDataService)
		{
			_uow = uow;
			_mapper = mapper;
			_availableDataService = availableDataService;
		}

		public async Task<IActionResult> Index()
		{

			var posts = await _uow.Posts.GetAll()
				.AsQueryable()
				.Where(p => p.IsActive)
				.OrderBy(p => Guid.NewGuid())
				.AsNoTracking()
				.Take(9)
				.ProjectTo<HomePostDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();

			if (User.Identity.IsAuthenticated)
			{
				var email = User.FindFirst("email").Value;
				var user = await _uow.Users.GetByEmailWithPostsAsync(email);

				foreach (var postDto in posts)
				{
					postDto.IsFavorite = user.FavoritePosts.Any(fp => fp.PostId == postDto.Id && fp.UserId == user.Id);
				}
			}


			var categories = await _uow.Categories.GetAll()
				.AsQueryable()
				.ProjectTo<CommonCategoryDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();

			HomeViewModel homeViewModel = new()
			{
				Categories = categories,
				Posts = posts,
				Filters = new()
				{

					AvailableTags = _availableDataService.GetAvailableTags().ToList(),
					AvailableCategories = _availableDataService.GetAvailableCategories().ToList(),
					AvailableCurrencies = _availableDataService.GetAvailableCurrencies().ToList(),
				}
			};
			return View(homeViewModel);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}