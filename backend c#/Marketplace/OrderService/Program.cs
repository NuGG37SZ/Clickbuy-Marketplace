using Microsoft.EntityFrameworkCore;
using OrderService.Client;
using OrderService.Db;
using OrderService.Repository;
using OrderService.Service;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrderContext>(options => options.UseSqlite(connection));
builder.Services.AddHttpClient<CartClient, CartClient>();
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepositoryImpl>();
builder.Services.AddScoped<IOrderProductService, OrderProductServiceImpl>();
builder.Services.AddScoped<IOrderRepository, OrderRepositoryImpl>();
builder.Services.AddScoped<IOrderService, OrderServiceImpl>();
builder.Services.AddScoped<IPointsRepository, PointsRepositoryImpl>();
builder.Services.AddScoped<IPointsService, PointsServiceImpl>();
builder.Services.AddScoped<IUserPointsRepository, UserPointsRepositoryImpl>();
builder.Services.AddScoped<IUserPointsService, UserPointsServiceImpl>();
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
