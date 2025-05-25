using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Common.Exceptions;
using CvAnalysisSystem.Common.GlobalResponses;
using CvAnalysisSystem.Common.GlobalResponses.Generics;
using CvAnalysisSystem.Common.Security;
using CvAnalysisSystem.Domain.Enums;
using CvAnalysisSystem.Repository.Common;
using MediatR;

namespace CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;

public class Update
{
    public record struct Command : IRequest<Result<UpdateDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Password { get; set; }
        public int? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
    }
    public sealed class Handler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<Command, Result<UpdateDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result<UpdateDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (currentUser == null) { throw new BadRequestException("The user data is invalid or the user does not exist."); }
            currentUser.Name = request.Name;
            currentUser.Surname = request.Surname;
            currentUser.FatherName = request.FatherName;
            currentUser.MobilePhone = request.MobilePhone;
            currentUser.Email = request.Email;
            currentUser.PasswordHash = request.Password;
            currentUser.Location = request.Location;
            if (Enum.TryParse<Gender>(request.Gender.ToString(), out var genderValue))
            {
                currentUser.Gender = genderValue;
            }
            else
            {
                currentUser.Gender = Gender.male;
            }
           
            if (!request.BirthDate.HasValue)
            {
                throw new BadRequestException("Birth date is required and cannot be null.");
            }
            else
            {
                currentUser.BirthDate = request.BirthDate.Value;
            }
            var hashPassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
            currentUser.PasswordHash = hashPassword;
            await _unitOfWork.UserRepository.Update(currentUser);
            var result = _mapper.Map<UpdateDto>(currentUser);
            return new Result<UpdateDto> { Data = result, Errors = [], IsSuccess = true };

        }
    }
}
