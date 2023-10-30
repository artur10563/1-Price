using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using OnePrice.Infrastructure.Data;
using OnePrice.UI.Extensions;
using OnePrice.UI.Middlewares;
using OnePrice.UI.Models.Mapping;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddLocalizationSupport();
builder.Services.AddStorage(builder.Configuration);
builder.Services.AddLogging();


builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddAutoMapper(typeof(TagProfile));
builder.Services.AddAutoMapper(typeof(CategoryProfile));
builder.Services.AddAutoMapper(typeof(PostProfile));

var app = builder.Build();

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

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


app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();




app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



app.Seed();
app.Run();