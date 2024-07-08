using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("sql_connection");
    
    options.UseSqlite(connectionString);

});

builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => 

options.LoginPath = "/Users/Login" );

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.FillTestData(app);

app.MapControllerRoute
(
    name : "post_details",
    pattern: "posts/details/{url}",
    defaults: new {controller ="Posts", action = "Details"}
);
app.MapControllerRoute
(
    name : "user_profile",
    pattern: "/users/profile/{username}",
    defaults: new {controller ="users", action = "profile"}
);
app.MapControllerRoute
(
    name : "post_by_tag",
    pattern: "posts/tag/{tag_url}",
    defaults: new {controller ="Posts", action = "Index"}
);
app.MapControllerRoute
(
    name : "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
