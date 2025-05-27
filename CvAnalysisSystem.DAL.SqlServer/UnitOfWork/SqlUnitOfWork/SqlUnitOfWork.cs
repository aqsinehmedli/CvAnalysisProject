using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.DAL.SqlServer.Infrastructure;
using CvAnalysisSystem.Repository.Common;
using CvAnalysisSystem.Repository.Repositories;

namespace CvAnalysisSystem.DAL.SqlServer.UnitOfWork
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public SqlUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        private SqlCvRepository? _cvRepository;
        public ICvRepository CvRepository => _cvRepository ?? new SqlCvRepository(_context);

        private SqlUserRepository? _userRepository;
        public IUserRepository UserRepository => _userRepository ?? new SqlUserRepository(_context);

        private RefreshTokenRepository? _refreshTokenRepository;
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository ?? new RefreshTokenRepository(_context);

        private SqlEmailVerificationRepository? _emailVerificationRepository;
        public IEmailVerificationRepository EmailVerificationRepository => _emailVerificationRepository ??= new SqlEmailVerificationRepository(_context);

        public async Task<int> SaveChange()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
