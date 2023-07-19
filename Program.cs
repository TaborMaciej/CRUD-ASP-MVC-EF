using lista7_zad1.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string connString = builder.Configuration.GetConnectionString("StudentServer");
builder.Services.AddDbContext<StudentsDbContext>(options =>
    options.UseMySql(connString, ServerVersion.AutoDetect(connString))
);

//Authentication setup
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
        {
            options.Cookie.Name = "LoginCookie";
            options.LoginPath = "/Home/Login"; // Customize the login page URL
            options.LogoutPath = "/Home/Logout"; // Customize the logout page URL
            options.AccessDeniedPath = "/Home"; // Customize the access denied page URL
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Adjust the expiration time
        });


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
