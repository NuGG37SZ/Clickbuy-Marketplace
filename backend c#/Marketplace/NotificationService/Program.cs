using NotificationService.Config;
using NotificationService.Models.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.Configure<MailSettings>(
        builder
            .Configuration
            .GetSection(nameof(MailSettings))
    );
builder.Services.AddTransient<IMailService, MailService>();

var app = builder.Build();
app.MapControllers();
app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
);
app.UseRouting();

app.Run();
