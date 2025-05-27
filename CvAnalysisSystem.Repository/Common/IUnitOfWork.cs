using CvAnalysisSystem.Repository.Repositories;

namespace CvAnalysisSystem.Repository.Common;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }
    public IRefreshTokenRepository RefreshTokenRepository { get; }

    public ICvRepository CvRepository { get; }
    public IEmailVerificationRepository EmailVerificationRepository { get; }
    Task<int> SaveChange();


}
