using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnePrice.Application.Repository;
using OnePrice.Infrastructure.Data;
using OnePrice.UI.Models;
using OnePrice.UI.Models.CommonDTOs;
using OnePrice.UI.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace OnePrice.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public HomeController(
			IUnitOfWork uow,
			IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
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



			var categories = await _uow.Categories.GetAll()
				.AsQueryable()
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