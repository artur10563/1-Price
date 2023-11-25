using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities.ChatEntities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.ChatDTOs;
using OnePrice.UI.Models.CommentDTOs;

namespace OnePrice.UI.Controllers
{
	[Authorize]
	[ServiceFilter(typeof(EnsureUserExistsAttribute))]
	public class ChatController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public ChatController(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithChatsAsync(email);

			var model = user.Chats
				.AsQueryable()
				.ProjectTo<ChatDTO>(_mapper.ConfigurationProvider)
				.ToList();

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Private(int chatId)
		{

			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithChatsAsync(email);


			if (!user.Chats.Any(c => c.ChatId == chatId)) return Ok("You dont have access to this chat");

			var chat = await _uow.Chats.GetFullByIdAsync(chatId);

			if (chat == null) return BadRequest();


			var model = new ChatViewModel()
			{
				AvailableChats = user.Chats
				.AsQueryable()
				.ProjectTo<ChatDTO>(_mapper.ConfigurationProvider)
				.ToList(),


				Chat = _mapper.Map<ChatDTO>(chat)
			};


			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Create(int receiverId)
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailAsync(email);

			if (user.Id == receiverId) return RedirectToAction(nameof(Index));

			if (user == null) return BadRequest();

			var chat = await _uow.Chats.GetByUserIdsAsync(user.Id, receiverId);

			if (chat == null)
			{
				var members = new List<UserChat>
				{
					new UserChat() { Chat = chat, UserId = user.Id },
					new UserChat() { Chat = chat, UserId = receiverId },
				};


				chat = new Chat
				{
					Title = "New chat",
					Members = members,
				};

				await _uow.Chats.AddAsync(chat);
				await _uow.SaveChangesAsync();
			}

			return RedirectToAction(nameof(Index), new { chatId = chat.Id });
		}

	}
}
