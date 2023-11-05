using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace OnePrice.UI.Controllers
{
	public class IdentityController : Controller
	{
		public IActionResult Logout()
		{
			return SignOut("Cookies", "oidc");
		}

		public async Task<IActionResult> Login()
		{
			return Challenge(new AuthenticationProperties
			{
				RedirectUri = "/"
			});
		}


		//Not implemented yet
		public IActionResult Register()
		{
			return Redirect("https://localhost:5001/account/register");
		}
	}
}
