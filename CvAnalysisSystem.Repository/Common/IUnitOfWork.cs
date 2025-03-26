using CvAnalysisSystem.Repository.Repositories;

namespace CvAnalysisSystem.Repository.Common;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }
    Task<int> SaveChange();
}
