using AuthProject.DBContext;
using AuthProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AuthDBContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthDBContext>();

builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<AuthDBContext>().AddDefaultTokenProviders();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapFallbackToFile("index.html", new StaticFileOptions()
{
    OnPrepareResponse = staticFileResponseContext =>
    {
        staticFileResponseContext.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
        staticFileResponseContext.Context.Response.Headers.Append("Expires", "-1");
    }
});

app.Run();
