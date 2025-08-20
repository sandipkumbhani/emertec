using Emertec.UI.Application.Extension;
using Emertec.UI.Domain.Model;
using Emertec.UI.Infrastructure.Extension;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var globalClass = new GlobalClass();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/Home/Denied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
    });

builder.Services.AddAuthorization();
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
//    options.AddPolicy("SupervisorPolicy", policy => policy.RequireRole("Supervisor"));
//});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationService();
builder.Services.AddEfcoreInfrastrucureService();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
//builder.Services.AddSingleton(globalClass);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    // Read a specific cookie
    var token = context.Request.Cookies["jwtToken"];
    globalClass.Token = token;
    //if(token != null)
    //{
    //    globalClass.Token = token;
    //}
    //else
    //{
    //    globalClass.Token = token;
    //}
    await next.Invoke();
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");
    
app.Run();
