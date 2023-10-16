using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_SHOP.Infrastructure.Data;
using E_SHOP.Application.Repository;
using E_SHOP.Infrastructure.Repository;
using E_SHOP.UI.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//потім перемістити в AddStorage
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddControllersWithViews();
builder.Services.AddStorage(builder.Configuration);

builder.Services.AddLogging();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();




app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



app.Seed();
app.Run();
