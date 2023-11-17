using OnePrice.Infrastructure.Data;
using OnePrice.UI.Extensions;
using OnePrice.UI.Middlewares;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentitySupport();


builder.Services.AddLocalizationSupport();
builder.Services.AddStorage(builder.Configuration);
builder.Services.AddAutoMapperStorage();

builder.Services.AddLogging();
builder.Services.AddScoped<EnsureUserExistsAttribute>();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

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
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");




app.Seed();
app.Run();
