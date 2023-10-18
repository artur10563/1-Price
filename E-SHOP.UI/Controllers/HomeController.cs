
using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SHOP.Application.Repository;
using E_SHOP.Infrastructure.Data;
using E_SHOP.UI.Models;
using E_SHOP.UI.Models.CommonDTOs;
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
		private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
			AppDbContext context,
			IUnitOfWork uow,
			IMapper mapper)
		{
			_logger = logger;
			_context = context;
			_uow = uow;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			Random random = new Random();

			

			var randomPosts = _context.Posts
				.ToList()
				.AsQueryable()
				.Where(p=>p.IsActive)
				.OrderBy(p => random.Next())
				.Take(9);

			var posts = await _context.Posts
				.Where(p=>p.IsActive)
				.ProjectTo<HomePostDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();



			var categories = await _context.Categories
				.ProjectTo<CommonCategoryDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();



			HomeViewModel homeViewModel = new() { Categories = categories, Posts = posts };

			return View(homeViewModel);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}