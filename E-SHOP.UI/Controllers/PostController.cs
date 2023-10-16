using E_SHOP.Domain.Entities;
using E_SHOP.Infrastructure.Data;
using E_SHOP.UI.Models.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
		public async Task<IActionResult> Add()
		{
			PostAddViewModel model = new PostAddViewModel();

			model.AvailableTags = await _context.Tags
				.Select(tag => new TagDTO() { Id = tag.Id, Name = tag.Name })
				.ToListAsync();


			model.AvailableCategories = await _context.Categories
				.Select(category => new CategoryDTO() { Id = category.Id, Name = category.Name })
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
					.Select(tag => new TagDTO() { Id = tag.Id, Name = tag.Name })
					.ToListAsync();

				postViewModel.AvailableCategories = await _context.Categories
					.Select(category => new CategoryDTO() { Id = category.Id, Name = category.Name })
					.ToListAsync();

				return View(postViewModel);
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

			_context.Posts.Add(newPost);
			_context.SaveChanges();

			TempData["AddStatus"] = "Post added successfully";
			return RedirectToAction("Add");
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
