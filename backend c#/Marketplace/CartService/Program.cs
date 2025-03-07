using CartService.Client;
using CartService.Db;
using CartService.Repository;
using CartService.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CartContext>(options => options.UseSqlite(connection));
builder.Services.AddScoped<ICartRepository, CartRepositoryImpl>();
builder.Services.AddScoped<ICartService, CartServiceImpl>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepositoryImpl>();
builder.Services.AddScoped<IFavoritesService, FavoritesServiceImpl>();
builder.Services.AddHttpClient<UserClient, UserClient>();
builder.Services.AddHttpClient<ProductClient, ProductClient>();
builder.Services.AddHttpClient<ProductSizesClient, ProductSizesClient>();
builder.Services.AddCors();
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRouting();
app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
);
app.UseEndpoints(endpoints => endpoints.MapControllers());


app.Run();
