using CvAnalysisSystem.Application;
using CvAnalysisSystem.Application.Security;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Application.Services.Concret;
using CvAnalysisSystem.Application.Services.Implementations;
using CvAnalysisSystem.Application.Services.Interfaces;
using CvAnalysisSystem.Common.Email;
using CvAnalysisSystem.DAL.SqlServer;
using CvAnalysisSystemProject;
using CvAnalysisSystemProject.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("MyConn");
builder.Services.AddSqlServerServices(connectionString!);
builder.Services.AddApplicationServices();
builder.Services.AddScoped<IUserContext, HttpUserContext>();
builder.Services.AddSingleton<IHttpContextAccessor , HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthenticationDependency(builder.Configuration);

builder.Services.AddSignalR(options => options.EnableDetailedErrors = true);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .WithOrigins("http://localhost:5184", "http://localhost:5185", "http://localhost:5193")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Smtp"));
builder.Services.AddScoped<IEmailService, EmailService>();
// STRIPE AÇARI KONFİQURASİYASI BURADA ƏLAVƏ OLUNUR
Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();
app.MapHub<CvAnalysisSystem.Application.Services.ChatHub>("/chatHub");

app.Run();
