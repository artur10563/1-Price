using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;
using OnePrice.Domain.Entities.ChatEntities;
using OnePrice.UI.Extensions;
using OnePrice.UI.Models.ChatDTOs;
using OnePrice.UI.Models.CommonDTOs;

namespace OnePrice.UI.Controllers
{
	[Authorize]
	[ServiceFilter(typeof(EnsureUserExistsAttribute))]
	public class ChatController : Controller
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IHtmlLocalizer<SharedResource> _localizer;

		public ChatController(IUnitOfWork uow, IMapper mapper, IHtmlLocalizer<SharedResource> localizer)
		{
			_uow = uow;
			_mapper = mapper;
			_localizer = localizer;
		}

		//TODO: Repalce with automapper
		private ICollection<ChatDTO>? GetChatDTOs(AppUser user)
		{
			return user.Chats
				.Select(x => x.Chat)
				.SelectMany(c => c.Members)
				.GroupBy(uc => uc.ChatId)
				.Select(group => new ChatDTO
				{
					ChatId = group.Key,
					Title = "YourTitle",
					Sender = _mapper.Map<CommonAppUserDTO>(group.FirstOrDefault(uc => uc.UserId == user.Id)?.User),
					Receiver = _mapper.Map<CommonAppUserDTO>(group.FirstOrDefault(uc => uc.UserId != user.Id)?.User)
				})
				.ToList();

		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithChatsAsync(email);

			var availableChats = GetChatDTOs(user);

			return View(availableChats);
		}

		[HttpGet]
		public async Task<IActionResult> Private(int chatId)
		{

			var email = User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithChatsAsync(email);


			if (!user.Chats.Any(c => c.ChatId == chatId))
			{
				TempData["ErrorMessage"] = _localizer["AccessDenied"].Value;
				return RedirectToAction("Index", "Profile");
			}
			var chat = await _uow.Chats.GetFullByIdAsync(chatId);

			if (chat == null) return BadRequest();


			var model = new ChatViewModel()
			{
				AvailableChats = GetChatDTOs(user),

				Chat = _mapper.Map<ChatDTO>(chat),
			};

			model.Chat.Sender = _mapper.Map<CommonAppUserDTO>(user);
			model.Chat.Receiver = _mapper.Map<CommonAppUserDTO>(chat.Members.Select(c => c.User).FirstOrDefault(u => u.Id != user.Id));





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

			return RedirectToAction(nameof(Private), new { chatId = chat.Id });
		}

	}
}
