using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Application.Services;
using CvAnalysisSystem.Common.Exceptions;
using CvAnalysisSystem.Common.GlobalResponses.Generics;
using CvAnalysisSystem.Common.Security;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Repository.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CvAnalysisSystem.DAL.SqlServer.Infrastructure;
namespace CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;

public class Login
{
    public class LoginRequest : IRequest<Result<LoginDto>>
    {
        public string? Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork,IConfiguration configuration) : IRequestHandler<LoginRequest, Result<LoginDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;



        public async Task<Result<LoginDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            //_loggerService.LogInfo($"Logging attempt : {request.Email}");
            User user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email) ?? throw new BadRequestException($"Invalid Email : {request.Email}");
            var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

            if (user.PasswordHash != hashedPassword)
            {
                //_loggerService.LogWarning($"Log in Fail  -User not found:{request.Email}");
                throw new BadRequestException("Invalid password");
            }

            if (user != null && hashedPassword == user.PasswordHash)
            {
                List<Claim> authClaim =
                [
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                 new Claim(ClaimTypes.Name , user.Name),
                 new Claim(ClaimTypes.Email, user.Email),
                ];

                JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                string refreshToken = TokenService.GenerateRefreshToken();

                LoginDto response = new() { AccessToken = tokenString, RefreshToken = refreshToken };

                Domain.Entities.RefreshToken saveRefreshToken = new()
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    ExpirationDate = DateTime.Now.AddDays(double.Parse(configuration.GetSection("JWT:RefreshTokenExpirationDays").Value!))
                };

                await _unitOfWork.RefreshTokenRepository.SaveRefreshToken(saveRefreshToken);
                await _unitOfWork.SaveChange();
                //_loggerService.LogInfo($"User entered website succesfully:{request.Email}");
                return new Result<LoginDto> { Data = response };
            }
            return new Result<LoginDto>()
            {
                Data = null,
                Errors = ["Login is failed"]
            };
        }
    }
}
