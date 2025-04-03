using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.DAL.SqlServer.UnitOfWork;
using CvAnalysisSystem.Repository.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CvAnalysisSystem.DAL.SqlServer;

public static class DependencyInjections
{
    public static IServiceCollection AddSqlServerServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, SqlUnitOfWork>();
        return services;
    }
}
