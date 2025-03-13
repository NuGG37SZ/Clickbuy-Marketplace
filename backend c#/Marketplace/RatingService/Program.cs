using Microsoft.EntityFrameworkCore;
using RatingService.Db;
using RatingService.Repository;
using RatingService.Service;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RatingContext>(options => options.UseSqlite(connection));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRatingProductRepository, RatingProductRepositoryImpl>();
builder.Services.AddScoped<IRatingProductService, RatingProductServiceImpl>();
builder.Services.AddScoped<IRatingSellerRepository, RatingSellerRepositoryImpl>();
builder.Services.AddScoped<IRatingSellerService, RatingSellerSerivceImpl>();
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
