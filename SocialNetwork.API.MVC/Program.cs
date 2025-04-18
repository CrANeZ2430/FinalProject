using Auth0.AspNetCore.Authentication;
using SocialNetwork.API.MVC.Services;
using SocialNetwork.Application;
using SocialNetwork.Infrastructure;
using SocialNetwork.Infrastructure.Middleware;
using SocialNetwork.Persistence.SocialNetworkDb;

var builder = WebApplication.CreateBuilder(args);

// small comment
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<CookiePolicyOptions>(options =>
  {
      options.MinimumSameSitePolicy = SameSiteMode.None;
  });
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.Scope = "openid profile email";
});

builder.Services.RegisterApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.RegisterSocialNetworkDbContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseCustomExceptionHandler(app.Environment);
}

app.UseStatusCodePagesWithReExecute("/Home/AccessDenied");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "getUserDetails",
    pattern: "{controller=Users}/{action=GetUserDetails}/{userId}");

app.MapControllerRoute(
    name: "getPostComments",
    pattern: "{controller=Posts}/{action=GetPostComments}/{postId}");

app.MapControllerRoute(
    name: "getUserPosts",
    pattern: "{controller=Posts}/{action=GetUserPosts}/{userId}");

app.MapControllerRoute(
    name: "getUserComments",
    pattern: "{controller=Comments}/{action=GetUserComments}/{userId}");

app.Run();
