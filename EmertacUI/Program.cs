using Emertac.UI.Application.Interface;
using Emertac.UI.Application.Service;
using Emertac.UI.Domain.Interface;
using Emertac.UI.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews(); 

// Add services to the container.
builder.Services.AddScoped<ICreateUserRepository, CreateUserRepository>();

builder.Services.AddScoped<ICreateUserService, CreateUserService>();

builder.Services.AddScoped<ILoginUserRepository, LoginUserRepository>();
builder.Services.AddScoped<ILoginUserService, LoginUserService>();
builder.Services.AddHttpClient<ICreateUserRepository, CreateUserRepository>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5010/api/");
});
builder.Services.AddHttpClient<ILoginUserRepository, LoginUserRepository>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5010/api/");
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
