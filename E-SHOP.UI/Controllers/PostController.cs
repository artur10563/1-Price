using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SHOP.Application.Repository;
using E_SHOP.Domain.Entities;
using E_SHOP.Infrastructure.Data;
using E_SHOP.UI.Models.PostDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace E_SHOP.UI.Controllers
{
	public class PostController : Controller
	{

		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly AppDbContext _context;
		public PostController(IUnitOfWork uow, IMapper mapper, AppDbContext context)
		{
			_uow = uow;
			_mapper = mapper;
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			PostAddViewModel model = new PostAddViewModel();

			model.AvailableTags = await _context.Tags
				.ProjectTo<TagDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();


			model.AvailableCategories = await _context.Categories
				.ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();


			if (TempData.ContainsKey("AddStatus"))
			{
				ViewData["AddStatus"] = TempData["AddStatus"];
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(PostAddViewModel postViewModel, IFormFile? image)
		{

			if (!ModelState.IsValid)
			{
				postViewModel.AvailableTags = await _context.Tags
					.ProjectTo<TagDTO>(_mapper.ConfigurationProvider)
					.ToListAsync();


				postViewModel.AvailableCategories = await _context.Categories
					.ProjectTo<CategoryDTO>(_mapper.ConfigurationProvider)
					.ToListAsync();

				return View(postViewModel);
			}


			PostAddDTO post = postViewModel.Post;

			Post newPost = new Post()
			{
				Title = post.Title,
				Description = post.Description,
				Price = post.Price,
				Currency = post.Currency,
				ImgPath = post.ImgPath,
				Category = _uow.Categories
					.FirstOrDefault(c => c.Id == post.CategoryId)
			};

			var selectedTags = _context.Tags
				.Where(tag => post.TagsId.Contains(tag.Id))
				.ToList();

			var newPostTag = new List<PostTag>();

			foreach (Tag tag in selectedTags)
			{
				newPostTag.Add(new PostTag { Post = newPost, Tag = tag });
			}
			newPost.Tags = newPostTag;

			//add normal img path with ImgHelper

			await _uow.Posts.AddAsync(newPost);
			await _uow.SaveChangesAsync();

			TempData["AddStatus"] = "Post added successfully";
			return RedirectToAction("Add");
		}

		public async Task<IActionResult> Get(string category)
		{
			if (string.IsNullOrWhiteSpace(category)) return BadRequest();


			List<PostDisplayDTO> posts =
				await _context.Posts
				.Where(p => p.IsActive == true)
				.Where(p => p.Category.Name.CompareTo(category) == 0)
				.ProjectTo<PostDisplayDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();

			//change to paginatedList later

			if (posts.Count == 0) return BadRequest();
			return View(posts);
		}


	}
}
