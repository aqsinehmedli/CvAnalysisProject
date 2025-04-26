using CvAnalysisSystem.DAL.SqlServer;
using CvAnalysisSystem.Application;
using CvAnalysisSystemProject.Security;
using CvAnalysisSystemProject.Middlewares;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database bağlantısı
var connectionString = builder.Configuration.GetConnectionString("MyConn");
builder.Services.AddSqlServerServices(connectionString!);

// Application xidmətləri
builder.Services.AddApplicationServices();

// Authentication xidməti
builder.Services.AddAuthenticationDependency(builder.Configuration);

// HttpContext üçün
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// CORS policy əlavə edilir
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .WithOrigins(
                "http://localhost:5173",
                "http://localhost:3000",
                "https://localhost:7087",
                "https://localhost:5188",
                "http://localhost:5188"
            )
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Stripe üçün ayarlar
var stripeSettings = builder.Configuration.GetSection("Stripe");
StripeConfiguration.ApiKey = stripeSettings["SecretKey"];

var app = builder.Build();

// Middleware konfiqurasiyası
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Development mühitindəyiksə, HTTPS redirect istifadə etmə (redirect problemi olmasın)
    // app.UseHttpsRedirection(); // BU SƏTİRİ İSTİFADƏ ETMİRİK!
}
else
{
    // Production mühitində HTTPS Redirect aktiv olsun
    app.UseHttpsRedirection();
}

// CORS burda aktivləşdirilir
app.UseCors("AllowAll");

// Authentication və Authorization
app.UseAuthentication();
app.UseAuthorization();

// Custom Middlewares
app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

// Controllerləri mapp edirik
app.MapControllers();

app.Run();
