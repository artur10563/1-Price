using E_SHOP.Application.Repository;
using E_SHOP.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_SHOP.Infrastructure.Data
{
	public static class RegistrationExtension
	{
		public static void AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection")
				?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			serviceCollection.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));



			serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
			serviceCollection.AddScoped<ITagRepository, TagRepository>();
			serviceCollection.AddScoped<ICategoryRepository, CategoryRepository>();
			serviceCollection.AddScoped<IPostRepository, PostRepository>();
		}
	}
}
