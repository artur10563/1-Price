using OnePrice.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OnePrice.Infrastructure.Data
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
							new Category { Name = "Electronics and Gadgets", ImgPath = "/img/category/c1.jpg" },
							new Category { Name = "Sports and Outdoors", ImgPath = "/img/category/c2.jpg" },
							new Category { Name = "Home and Living", ImgPath = "/img/category/c3.jpg" }
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

					if (context.Currencies.Count() == 0)
					{
						context.Currencies.AddRange(
							new Currency { FullName = "Dollar", Symbol = '$', Code = "USD" },
							new Currency { FullName = "Hryvnia", Symbol = '₴', Code = "UAH" },
							new Currency { FullName = "Euro", Symbol = '€', Code = "EUR" },
							new Currency { FullName = "Yen", Symbol = '¥', Code = "JPY" }
							);
						context.SaveChanges();
					}
					
					if (context.ComplaintStatuses.Count() == 0)
					{
						context.ComplaintStatuses.AddRange(
							new ComplaintStatus() { Name = "Pending" },
							new ComplaintStatus() { Name = "In Progress" },
							new ComplaintStatus() { Name = "Closed" }
							);
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
