using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Repository.Common;
using System;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.Services.Concret
{
    public class UserService : IUserService
    {
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }

        public async Task<UserProfileDto> GetCurrentUser()
        {
            int userId = _userContext.MustGetUserId();

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            return new UserProfileDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
            };
        }
    }
}
