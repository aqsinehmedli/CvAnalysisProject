using CvAnalysisSystem.Application.Services;
using CvAnalysisSystem.Common.GlobalResponses.Generics;
using CvAnalysisSystem.Repository.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;

public class RefreshToken
{
    public class RefreshTokenReuqest : IRequest<Result<string>>
    {
        public string Token { get; set; }
    }

    public class Handler(IUnitOfWork unitOfWork, IConfiguration configuration) : IRequestHandler<RefreshTokenReuqest, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;

        public async Task<Result<string>> Handle(RefreshTokenReuqest request, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.RefreshTokenRepository.GetRefreshToken(request.Token);
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(refreshToken.UserId);

            if (refreshToken == null || refreshToken.ExpirationDate < DateTime.Now)
            {
                throw new UnauthorizedAccessException();
            }


            List<Claim> authClaim =
            [
                    new Claim(ClaimTypes.NameIdentifier , currentUser.Id.ToString()),
                    new Claim(ClaimTypes.Name , currentUser.Name),
                new Claim(ClaimTypes.Email, currentUser.Email),
                ];

            JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new Result<string> { Data = tokenString };
        }
    }
}
