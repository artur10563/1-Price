using OnePrice.UI.Models.Mapping;

namespace OnePrice.UI.Extensions
{
	public static class AutoMapperExtension
	{
		public static IServiceCollection AddAutoMapperStorage(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddAutoMapper(typeof(TagProfile));
			serviceCollection.AddAutoMapper(typeof(CategoryProfile));
			serviceCollection.AddAutoMapper(typeof(PostProfile));
			serviceCollection.AddAutoMapper(typeof(CurrencyProfile));
			serviceCollection.AddAutoMapper(typeof(CommentProfile));
			serviceCollection.AddAutoMapper(typeof(UserProfile));
			serviceCollection.AddAutoMapper(typeof(ChatProfile));

			return serviceCollection;
		}
	}
}
