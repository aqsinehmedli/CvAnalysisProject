using CvAnalysisSystem.DAL.SqlServer;
using CvAnalysisSystem.Application;
using CvAnalysisSystemProject.Security;
using CvAnalysisSystemProject.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Stripe;

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
                "http://localhost:5176",
                "http://localhost:5182",
                "https://localhost:5182",
                "http://localhost:5182",
                "http://localhost:5174",
                "http://localhost:5173",

                "http://localhost:3000",
                "https://localhost:7087",
                "https://localhost:5188",
                "http://localhost:5188"


            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // SignalR üçün vacib
    });
});

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
