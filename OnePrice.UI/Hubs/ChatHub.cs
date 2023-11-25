using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities.ChatEntities;
using OnePrice.UI.Extensions;

namespace OnePrice.UI.Hubs
{
	[Authorize]
	[ServiceFilter(typeof(EnsureUserExistsAttribute))]
	public class ChatHub : Hub
	{
		private readonly IUnitOfWork _uow;

		public ChatHub(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public async Task JoinChat(string chatId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
		}

		public async Task SendMessage(string group,
			string message)
		{
			var email = Context.User.FindFirst("email").Value;
			var user = await _uow.Users.GetByEmailWithChatsAsync(email);
			int groupId = int.Parse(group);

			var chat = await _uow.Chats.GetFullByIdAsync(groupId);
			var newMessage = new Message()
			{
				Chat = chat,
				Author = user,
				Content = message,
			};
			chat.Messages.Add(newMessage);
			await _uow.SaveChangesAsync();


			await Clients.Groups(group).SendAsync("ReceiveMessage", user.Nickname, message, newMessage.CreatedAt.ToLocalTime());
		}
	}

}
