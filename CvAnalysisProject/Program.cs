using CvAnalysisSystem.Application;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Application.Services.Concret;
using CvAnalysisSystem.Application.Services.Implementations;
using CvAnalysisSystem.Application.Services.Interfaces;
using CvAnalysisSystem.Common.Email;
using CvAnalysisSystem.DAL.SqlServer;
using CvAnalysisSystemProject.Infrastructure;
using CvAnalysisSystemProject.Middlewares;
using CvAnalysisSystemProject.Security;
using Stripe;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Database bağlantısı
var connectionString = builder.Configuration.GetConnectionString("MyConn");
builder.Services.AddSqlServerServices(connectionString!);

// 3. Application xidmətləri
builder.Services.AddApplicationServices();

// 4. Authentication xidməti (JWT ilə)
builder.Services.AddAuthenticationDependency(builder.Configuration);
builder.Services.AddScoped<IUserContext, HttpContextUser>();


// 5. HttpContext üçün
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// 6. SignalR əlavə et
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

// 7. CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5184",
                "http://localhost:5185",
                "http://localhost:5193"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // SignalR üçün vacib
    });
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddScoped<IEmailService, EmailService>();



// 8. Stripe üçün ayarlar
var stripeSettings = builder.Configuration.GetSection("Stripe");
StripeConfiguration.ApiKey = stripeSettings["SecretKey"];

var app = builder.Build();

// 9. Middleware konfiqurasiyası
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // HTTPS redirect dev-də lazım deyil
    // app.UseHttpsRedirection();
}
else
{
    app.UseHttpsRedirection();
}

// 10. CORS aktivləşdir
app.UseCors("AllowAll");

// 11. Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 12. Custom Middlewares
app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

// 13. Controllerləri və SignalR hub'ı mapp et
app.MapControllers();
app.MapHub<CvAnalysisSystem.Application.Services.ChatHub>("/chatHub");

app.Run();
