using System.Security.Claims;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServer;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var artur10563 = userMgr.FindByNameAsync("artur10563").Result;
            if (artur10563 == null)
            {
                artur10563 = new ApplicationUser
                {
                    UserName = "artur10563",
                    Email = "artur10563@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(artur10563, "Pass123!").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(artur10563, new Claim[]{
                            new Claim("role", "admin")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("artur10563 created");
            }
            else
            {
                Log.Debug("artur10563 already exists");
            }
        }
    }
}
