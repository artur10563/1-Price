using E_SHOP.Domain.Entities;
using E_SHOP.Infrastructure.Data;
using E_SHOP.UI.Models.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace E_SHOP.UI.Controllers
{
	public class PostController : Controller
	{
		private readonly AppDbContext _context;
		public PostController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Add()
		{
			PostAddViewModel model = new PostAddViewModel();

			model.AvailableTags = _context.Tags
				.Select(tag => new TagDTO() { Id = tag.Id, Name = tag.Name })
				.ToList();


			model.AvailableCategories = _context.Categories
				.Select(category => new CategoryDTO() { Id = category.Id, Name = category.Name })
				.ToList();

			return View(model);
		}

		[HttpPost]
		public IActionResult Add(PostAddViewModel postViewModel, IFormFile? image)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			PostDTO post = postViewModel.Post;

			Post newPost = new Post()
			{
				Title = post.Title,
				Description = post.Description,
				Price = post.Price,
				Currency = post.Currency,
				ImgPath = post.ImgPath,
				Category = _context.Categories
					.FirstOrDefault(c => c.Id == post.CategoryId)
			};

			//_context.Posts.Add(newPost);
			//_context.SaveChanges();

			var selectedTagIds = post.Tags.Select(tagDto => tagDto.Id).ToList();
			var selectedTags = _context.Tags
				.Where(tag => selectedTagIds.Contains(tag.Id))
				.ToList();

			foreach (var tag in selectedTags)
			{
				newPost.Tags.Add(new PostTag { Post = newPost, Tag = tag });
			}

			_context.Posts.Add(newPost);


			return Ok(newPost);
			_context.Posts.Add(newPost);
			ViewData["AddStatus"] = "Post added successfully";
			return View();
		}

		public IActionResult Get(string category)
		{
			if (string.IsNullOrWhiteSpace(category)) return BadRequest();
			if (_context.Categories.Any(c => c.Name.CompareTo(category) == 0)) return BadRequest();

			List<PostDTO> posts =
				_context.Posts
				.Where(p => p.IsActive == true)
				.Include(p => p.Category)
				.Where(p => p.Category.Name.CompareTo(category) == 0)
				.Select(p => new PostDTO
				{
					Title = p.Title,
					Description = p.Description,
					Price = p.Price,
					Currency = p.Currency,
					ImgPath = p.ImgPath,
				}).ToList();

			//change to paginatedList later

			if (posts.Count < 0) return BadRequest();
			return View(posts);
		}

	}
}
