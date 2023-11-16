using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.CommentDTOs;

namespace OnePrice.UI.Controllers
{
	public class CommentController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public CommentController(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		[HttpPost]
		[Authorize]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> AddComment(CommentAddDTO commentData)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
				return  BadRequest(new { Errors = errors });
			}
			return Ok("new comment");

			////need to add validation (remake as model?)
			//var email = User.FindFirst("email").Value;
			//var author = await _uow.Users.GetByEmailAsync(email);

			//Comment comment = new Comment()
			//{
			//	Author = author,
			//	PostId = postId,
			//	Content = content,
			//};

			//CommentDisplayDTO newComment = _mapper.Map<CommentDisplayDTO>(comment);

			//await _uow.SaveChangesAsync();
			//return PartialView("_CommentPartial", newComment);
		}
	}
}
