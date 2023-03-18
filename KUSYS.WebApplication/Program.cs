using KUSYS.UI.UIManager;
using KUSYS.WebApplication.Models;
using KUSYS.WebApplication.UIHandler;
using Microsoft.AspNetCore.Authentication.Cookies;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//});
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/Account/Login");
builder.Services.AddAuthorization(options =>
{
	// options.ToJToken
});

#region dependicies
builder.Services.AddTransient<IdentityUserManager>();
builder.Services.AddSingleton<IProxyManager, ProxyManager>();
builder.Services.AddHttpClient<IProxyManager, ProxyManager>(client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiConfigure:Url"]);
});
builder.Services.AddHealthChecks();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();
//app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
