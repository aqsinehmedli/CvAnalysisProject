using CvAnalysisSystem.DAL.SqlServer;
using CvAnalysisSystem.Application;
using CvAnalysisSystemProject.Security;
using CvAnalysisSystemProject.Middlewares;
using Stripe;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("MyConn");

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSqlServerServices(connectionString!);
builder.Services.AddApplicationServices();
builder.Services.AddAuthenticationDependency(builder.Configuration);
var stripeSettings = builder.Configuration.GetSection("Stripe");
StripeConfiguration.ApiKey = stripeSettings["SecretKey"];
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100 MB
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.WithOrigins ("http://localhost:5181")  // Frontend'inizin portunu buraya ekleyin
               .AllowAnyMethod()  // Tüm HTTP metotlarına izin verir
               .AllowAnyHeader()); // Tüm başlıklara izin verir
});
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins"); // Buraya ekleyin

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
