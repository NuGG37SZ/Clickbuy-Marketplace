using Microsoft.EntityFrameworkCore;
using UserService.Model.Db;
using UserService.Model.Repository;
using UserService.Model.Service;

var builder = WebApplication.CreateBuilder(args);
SQLitePCL.Batteries.Init();
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserContext>(options => options.UseSqlite(connection));
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.UseCors(builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                          .AllowAnyHeader());
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
