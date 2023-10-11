
using E_SHOP.Infrastructure.Data;
using E_SHOP.UI.Models;
using E_SHOP.UI.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_SHOP.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AppDbContext _context;
		public HomeController(ILogger<HomeController> logger, AppDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			Random random = new Random();

			var randomPosts = _context.Posts
				.ToList()
				.AsQueryable()
				.OrderBy(p => random.Next())
				.Take(9);

			var postsModel = randomPosts.Select(p => new HomePostDTO()
			{
				Title = p.Title,
				Description = p.Description,
				ImgPath = p.ImgPath,
				Currency = p.Currency,
				Price = p.Price
			}).ToList();

			var categoryModel = _context.Categories.Select(c => new HomeCategoryDTO()
			{
				Name = c.Name,
				ImgPath = c.ImgPath
			}).ToList();

			HomeViewModel homeViewModel = new() { Categories = categoryModel, Posts = postsModel };

			return View(homeViewModel);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}