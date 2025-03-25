using CvAnalysisSystem.Repository.Repositories;

namespace CvAnalysisSystem.Repository.Common;

public interface IUnitOfWork
{
    public IUserRepository _userRepository { get; }
}
