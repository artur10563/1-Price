using Microsoft.AspNetCore.Mvc.Razor;

namespace OnePrice.UI.Extensions
{
	public static class LocalizationExtension
	{
		public static IServiceCollection AddLocalizationSupport(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddLocalization(options => options.ResourcesPath = "Resources");

			serviceCollection.Configure<RequestLocalizationOptions>(options =>
			{
				options.AddSupportedUICultures("en", "uk");
				options.AddSupportedCultures("en", "uk");
				options.SetDefaultCulture("en");
				options.FallBackToParentUICultures = true;

			});


			serviceCollection.AddControllersWithViews()
				.AddViewLocalization()
				.AddDataAnnotationsLocalization();

			return serviceCollection;
		}
	}
}
