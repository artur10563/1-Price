using OnePrice.Application.Repository;
using OnePrice.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace OnePrice.Infrastructure.Data
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
			serviceCollection.AddScoped<IAppUserRepository, AppUserRepository>();
			serviceCollection.AddScoped<ICurrencyRepository, CurrencyRepository>();
			serviceCollection.AddScoped<ICommentRepository, CommentRepository>();
			serviceCollection.AddScoped<IChatRepository, ChatRepository>();
			serviceCollection.AddScoped<IComplaintStatusRepository, ComplaintStatusRepository>();
			serviceCollection.AddScoped<IComplaintRepository, ComplaintRepository>();
		}
	}
}
