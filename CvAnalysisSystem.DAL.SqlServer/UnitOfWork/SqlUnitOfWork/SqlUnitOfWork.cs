using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.DAL.SqlServer.Infrastructure;
using CvAnalysisSystem.Repository.Common;
using CvAnalysisSystem.Repository.Repositories;

namespace CvAnalysisSystem.DAL.SqlServer.UnitOfWork.SqlUnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;
    public SqlUserRepository _userRepository;

    public IUserRepository UserRepository => _userRepository ?? new SqlUserRepository(_context);
}
