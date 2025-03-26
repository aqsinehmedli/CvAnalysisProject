using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.DAL.SqlServer.Infrastructure;
using CvAnalysisSystem.Repository.Common;
using CvAnalysisSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CvAnalysisSystem.DAL.SqlServer.UnitOfWork.SqlUnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;
    public SqlUserRepository _userRepository;
    public RefreshTokenRepository _refreshTokenRepository;
    public IUserRepository UserRepository => _userRepository ?? new SqlUserRepository(_context);

    public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository ?? new RefreshTokenRepository(_context);

    public async Task<int> SaveChange()
    {
        return await _context.SaveChangesAsync();
    }
}