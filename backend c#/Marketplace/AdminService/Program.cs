using AdminService.Model.Db;
using AdminService.Model.Repositroy;
using AdminService.Model.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AdminContext>(options => options.UseSqlite(connection));
builder.Services.AddScoped<ICategoryReportRepository, CategoryReportRepositoryImpl>();
builder.Services.AddScoped<ICategoryReportService, CategoryReportServiceImpl>();
builder.Services.AddScoped<IReportRepository, ReportRepositoryImpl>();
builder.Services.AddScoped<IReportService, ReportServiceImpl>();
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
