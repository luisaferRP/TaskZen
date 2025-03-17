using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TaskZen.Data;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var Connection = Environment.GetEnvironmentVariable("CONNECTION_STRING");

//Cadena de conexión de sql server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Connection));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
