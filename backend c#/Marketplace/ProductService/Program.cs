using Microsoft.EntityFrameworkCore;
using ProductService.Db;
using ProductService.Client;
using ProductService.Repository;
using ProductService.Service;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlite(connection));
builder.Services.AddScoped<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();
builder.Services.AddScoped<IUserClient, UserClient>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
builder.Services.AddSingleton<HttpClient, HttpClient>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
