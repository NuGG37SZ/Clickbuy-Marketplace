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
builder.Services.AddHttpClient<IUserClient, UserClient>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<IBrandRepository, BrandRepositoryImpl>();
builder.Services.AddScoped<IBrandService, BrandServiceImpl>();
builder.Services.AddScoped<ISubcategoriesRepository, SubcategoriesRepositoryImpl>();
builder.Services.AddScoped<ISubcategoriesService, SubcategoriesServiceImpl>();
builder.Services.AddScoped<IBrandSubcategoriesRepository, BrandSubcategoriesRepositoryImpl>();
builder.Services.AddScoped<IBrandSubcategoriesService, BrandSubcategoriesServiceImpl>();
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
