using E_SHOP.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace E_SHOP.Infrastructure.Data
{
	public static class SeedingExtensions
	{
		public static void Seed(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var context = scope.ServiceProvider.GetService<AppDbContext>();

				try
				{
					context.Database.EnsureCreated();



					//Default categories: Electronics and Gadget, Sports and Outdoors, Home and Living 
					if (context.Categories.Count() == 0)
					{

						context.Categories.AddRange(
							new Category { Name = "Electronics and Gadgets", ImgPath = "https://www.enrgtech.co.uk/blog/wp-content/uploads/2022/12/jbareham_160418_0931_0086_FINAL_NO_BUFFER_5MB_02.0.jpg" },
							new Category { Name = "Sports and Outdoors", ImgPath = "https://www.listchallenges.com/f/lists/83296a8f-1b97-41af-b9cb-7d239d45decb.jpg" },
							new Category { Name = "Home and Living", ImgPath = "https://www.thespruce.com/thmb/P4hBQtEPZVrrWPdbtXy7-wv9fiE=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/GettyImages-1161177015-f1de4ba58a6c4f50969d9119d80405a6.jpg" }
						   );

						context.SaveChanges();

					}

					if (context.Tags.Count() == 0)
					{
						context.Tags.AddRange(
							new Tag { Name = "Used" },
							new Tag { Name = "New" },
							new Tag { Name = "Free" },
							new Tag { Name = "Negotiable price" },
							new Tag { Name = "Free shipping" },
							new Tag { Name = "Exchange possible" },
							new Tag { Name = "Cash on Delivery" },
							new Tag { Name = "Urgent sale" });

						context.SaveChanges();
					}
				}
				catch (Exception ex)
				{

					throw new Exception(ex.Message);
				}

			}
		}
	}
}
