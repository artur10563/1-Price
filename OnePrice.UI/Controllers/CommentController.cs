using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
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
		private readonly IHtmlLocalizer<SharedResource> _localizer;
		public CommentController(IUnitOfWork uow, IMapper mapper, IHtmlLocalizer<SharedResource> localizer)
		{
			_uow = uow;
			_mapper = mapper;
			_localizer = localizer;
		}

		[HttpPost]
		[ServiceFilter(typeof(EnsureUserExistsAttribute))]
		public async Task<IActionResult> AddComment(CommentAddDTO commentData)
		{
			if (!User.Identity.IsAuthenticated) return BadRequest(new
			{
				Errors = new[] { _localizer["LoginRequired"].Value }
			});

			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
				return BadRequest(new
				{
					Errors = errors
				});
			}

			var email = User.FindFirst("email").Value;
			var author = await _uow.Users.GetByEmailAsync(email);

			//Comment for database
			var comment = _mapper.Map<Comment>(commentData);
			comment.Author = author;

			await _uow.Comments.AddAsync(comment);
			await _uow.SaveChangesAsync();

			var newComment = _mapper.Map<CommentDisplayDTO>(comment);

			return PartialView("_CommentPartial", newComment);
		}
	}
}
