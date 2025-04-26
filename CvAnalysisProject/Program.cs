using CvAnalysisSystem.DAL.SqlServer;
using CvAnalysisSystem.Application;
using CvAnalysisSystemProject.Security;
using CvAnalysisSystemProject.Middlewares;
<<<<<<< HEAD
using Stripe;
=======
using CvAnalysisSystem.Application.Services;
>>>>>>> feature/OpenAiPdf
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
<<<<<<< HEAD
var stripeSettings = builder.Configuration.GetSection("Stripe");
StripeConfiguration.ApiKey = stripeSettings["SecretKey"];
=======
builder.Services.AddScoped<IPdfReaderService, PdfReaderService>();

>>>>>>> feature/OpenAiPdf
var app = builder.Build();

// Configure the HTTP request pipeline.
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
