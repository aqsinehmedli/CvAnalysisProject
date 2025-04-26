using Amazon.ElasticMapReduce.Model;
using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Common.Exceptions;
using CvAnalysisSystem.Common.GlobalResponses.Generics;
using CvAnalysisSystem.Common.Security;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Repository.Common;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;

public class Register
{
    public record struct Command : IRequest<Result<RegisterDto>>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Password { get; set; }
        public int? UserRoles { get; set; }
        public int? Gender { get; set; }
        [DataType(DataType.Date)]
        [SwaggerSchema(Format = "date")]
        public DateTime? BirthDate { get; set; }
    }
    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, Result<RegisterDto>>

    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;


       public async Task<Result<RegisterDto>> Handle(Command request, CancellationToken cancellationToken)
{
    try
    {
        var isExist = await _unitOfWork.UserRepository.GetByEmailAsync(request.Email);
        if (isExist != null) 
        { 
            throw new BadRequestException("User already registered with provided email!"); 
        }

        var newUser = _mapper.Map<User>(request);
        var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
        newUser.PasswordHash = hashPassword;
        await _unitOfWork.UserRepository.RegisterAsync(newUser);
        await _unitOfWork.SaveChange(); // SAVE burada çağırılmalıdır!

        var response = _mapper.Map<RegisterDto>(newUser);
        return new Result<RegisterDto> { Data = response, IsSuccess = true, Errors = [] };
    }
    catch (Exception ex)
    {
        throw new InternalServerException($"Register failed: {ex.Message}");
    }
}

    }
}
