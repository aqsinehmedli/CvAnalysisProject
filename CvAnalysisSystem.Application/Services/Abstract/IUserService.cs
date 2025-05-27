using CvAnalysisSystem.Application.CQRS.Users.DTOs;

namespace CvAnalysisSystem.Application.Services.Abstract;

public interface IUserService
{
    Task<UserProfileDto> GetCurrentUser();
}
