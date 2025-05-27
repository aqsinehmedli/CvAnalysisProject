using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CvAnalysisSystem.DAL.SqlServer.Infrastructure
{
    public class SqlEmailVerificationRepository(AppDbContext context) : IEmailVerificationRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddAsync(EmailVerification entity)
        {
            await _context.EmailVerifications.AddAsync(entity);
        }

        public async Task<EmailVerification?> GetValidCodeAsync(string email, string code)
        {
            return await _context.EmailVerifications
                .Where(x => x.Email == email && x.Code == code && !x.IsUsed && x.ExpireAt > DateTime.UtcNow)
                .OrderByDescending(x => x.ExpireAt)
                .FirstOrDefaultAsync();
        }
    }
}
