using CvAnalysisSystem.Domain.Entities;

namespace CvAnalysisSystem.Repository.Repositories;

public interface IRefreshTokenRepository
{
    Task SaveRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken> GetRefreshToken(string token);
}
