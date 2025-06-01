using System.Security.Claims;
using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;

namespace CvAnalysisSystem.Application.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<UserProfileDto> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var userProfile = new UserProfileDto
            {
                Id = int.TryParse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id) ? id : 0,
                Name = user?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty,
                Surname = user?.FindFirst(ClaimTypes.Surname)?.Value ?? string.Empty,
                Email = user?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty
            };

            return Task.FromResult(userProfile);
        }
    }
}
