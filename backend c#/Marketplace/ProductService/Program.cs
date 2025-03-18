using Microsoft.EntityFrameworkCore;
using ProductService.Model.Repository;
using ProductService.Model.Service;
using ProductService.Model.Db;
using ProductService.Model.Client;

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
builder.Services.AddScoped<IProductSizesRepository, ProductSizesRepositoryImpl>();
builder.Services.AddScoped<IProductSizesService, ProductSizesServiceImpl>();
builder.Services.AddControllersWithViews();
builder.Services.AddCors();
var app = builder.Build();

app.UseRouting();
    app.UseCors(builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
    );
    app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
