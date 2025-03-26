using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

public class RefreshTokenRepository(AppDbContext appDbContext) : IRefreshTokenRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<RefreshToken> GetRefreshToken(string token)
    {
        return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await _appDbContext.RefreshTokens.AddAsync(refreshToken);
    }
}