using Microsoft.AspNetCore.Mvc.Filters;
using OnePrice.Application.Repository;
using OnePrice.Domain.Entities;

namespace OnePrice.UI.Extensions
{
	public class EnsureUserExistsAttribute : ActionFilterAttribute
	{
		private readonly IUnitOfWork _uow;
		public EnsureUserExistsAttribute(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var contextUser = context.HttpContext.User;
			if (contextUser.Identity.IsAuthenticated)
			{
				var userEmail = contextUser.FindFirst("email")?.Value;
				var userName = contextUser.FindFirst("name")?.Value;
				if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(userName))
				{
					var user = await _uow.Users.GetByEmailAsync(userEmail);

					if (user == null)
					{
						user = new AppUser() { Email = userEmail, Nickname = userName };
						await _uow.Users.AddAsync(user);
						await _uow.SaveChangesAsync();
					}
				}
			}

			await next();
		}

	}
}
