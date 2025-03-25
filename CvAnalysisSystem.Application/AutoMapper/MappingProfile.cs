using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Domain.Entities;
using static CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.Register;

namespace CvAnalysisSystem.Application.AutoMapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        #region User
        CreateMap<Command, User>();
        CreateMap<User, RegisterDto>();
        #endregion
    }
}
