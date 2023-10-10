using E_SHOP.Domain.Entities;
using E_SHOP.Infrastructure.Data;
using E_SHOP.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_SHOP.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly AppDbContext _db;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			Category category = _db.Categories.FirstOrDefault(x => x.Id == 1);
			ICollection<Tag> tags = _db.Tags.ToList();
			//_db.Posts.Add(new Post()
			//{
			//	Title = "test",
			//	Description = "testesttestetetwtwetewtewtewtewt",
			//	Price = 432,
			//	Category = category,
			//	Currency = Domain.Enums.Currency.EUR,
			//	Tags = tags
			//});
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}