using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Common.Exceptions;
using CvAnalysisSystem.Common.GlobalResponses.Generics;
using CvAnalysisSystem.Common.Security;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Domain.Helpers;
using CvAnalysisSystem.Repository.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;

public class Login
{
    public class LoginRequest : IRequest<Result<LoginDto>>
    {
        public string? Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IConfiguration configuration) : IRequestHandler<LoginRequest, Result<LoginDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;


        public async Task<Result<LoginDto>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            User user = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email) ?? throw new BadRequestException($"Invalid Email : {request.Email}");
            var hashedPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);

            if (user.PasswordHash != hashedPassword)
            {
                throw new BadRequestException("Invalid password");
            }

            if (user != null && hashedPassword == user.PasswordHash)
            {
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                };
                
                JwtSecurityToken token = TokenService.CreateToken(authClaim, _configuration);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                LoginDto response = new() { AccessToken = tokenString };

                await _unitOfWork.SaveChange();
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
