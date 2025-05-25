using AutoMapper;
using CvAnalysisSystem.Application.AutoMapper;
using CvAnalysisSystem.Application.Behaviors;
using CvAnalysisSystem.Application.Services;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Application.Services.Concret;
using CvAnalysisSystem.DAL.SqlServer.Infrastructure;
using CvAnalysisSystem.DAL.SqlServer.UnitOfWork;
using CvAnalysisSystem.Repository.Common;
using CvAnalysisSystem.Repository.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace CvAnalysisSystem.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
        services.AddScoped<ICvRepository, SqlCvRepository>();
        services.AddScoped<IPDFService, PDFService>();
        services.AddTransient<ClassicTemplateStrategy>();
        services.AddTransient<ModernTemplateStrategy>();
        services.AddTransient<CvService>();
        services.AddTransient<ICvTemplateStrategy, ClassicTemplateStrategy>();
        services.AddTransient<ICvTemplateStrategyResolver, CvTemplateResolver>();
        services.AddScoped<IPdfReaderService, PdfReaderService>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });
        return services;
    }

}
