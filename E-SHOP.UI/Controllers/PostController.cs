using AutoMapper;
using AutoMapper.QueryableExtensions;
using E_SHOP.Application.Repository;
using E_SHOP.Domain.Entities;
using E_SHOP.Infrastructure.Data;
using E_SHOP.Infrastructure.Repository;
using E_SHOP.UI.Models.CommonIdDTOs;
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
				.ProjectTo<CommonIdTagDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();


			model.AvailableCategories = await _context.Categories
				.ProjectTo<CommonIdCategoryDTO>(_mapper.ConfigurationProvider)
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
					.ProjectTo<CommonIdTagDTO>(_mapper.ConfigurationProvider)
					.ToListAsync();


				postViewModel.AvailableCategories = await _context.Categories
					.ProjectTo<CommonIdCategoryDTO>(_mapper.ConfigurationProvider)
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

			var newPostTag = selectedTags.Select(t => new PostTag { Post = newPost, Tag = t }).ToList();

			newPost.Tags = newPostTag;

			//add normal img path with ImgHelper

			await _uow.Posts.AddAsync(newPost);
			await _uow.SaveChangesAsync();


			TempData["AddStatus"] = "Post added successfully";
			return RedirectToAction("Add");
		}

		[HttpGet]
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


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			//PostTag
			Post post = await _uow.Posts.GetByIdWithTagsAsync(id);
			if (post == null) return NotFound();


			PostEditViewModel model = new PostEditViewModel();

			model.AvailableTags = await _context.Tags
				.ProjectTo<CommonIdTagDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();


			model.AvailableCategories = await _context.Categories
				.ProjectTo<CommonIdCategoryDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();

			//PostAddDTO
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
				postViewModel.AvailableTags = await _context.Tags
				.ProjectTo<CommonIdTagDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();


				postViewModel.AvailableCategories = await _context.Categories
					.ProjectTo<CommonIdCategoryDTO>(_mapper.ConfigurationProvider)
					.ToListAsync();

				return View(postViewModel);
			}

			var editedPost = postViewModel.Post;
			var toEdit = await _uow.Posts.GetByIdAsync(editedPost.Id);

			if (toEdit == null) { return NotFound(); }



			toEdit.Title = editedPost.Title;
			toEdit.Description = editedPost.Description;
			toEdit.Price = editedPost.Price;
			toEdit.Currency = editedPost.Currency;
			toEdit.IsActive = editedPost.IsActive;
			toEdit.CategoryId = editedPost.CategoryId;

			_context.PostTags.RemoveRange(
				_context.PostTags
				.Where(pt => pt.PostId == toEdit.Id)
				.ToList());
			toEdit.Tags = editedPost.TagsId.Select(tagId => new PostTag { PostId = toEdit.Id, TagId = tagId }).ToList();


			_uow.Posts.Update(toEdit);
			await _uow.SaveChangesAsync();

			TempData["EditStatus"] = "Post edited successfully";

			return RedirectToAction("Edit", toEdit.Id);
		}
	}
}
