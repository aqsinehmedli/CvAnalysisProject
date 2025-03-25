using CvAnalysisSystem.Domain.Entities;

namespace CvAnalysisSystem.Repository.Repositories;

public interface IUserRepository
{
    Task RegisterAsync(User user);
    Task Update(User user);
    void Remove(int id);
    IQueryable<User> GetAll();
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);
}
