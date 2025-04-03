using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
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

        #region Update
        CreateMap<CQRS.Users.Handlers.Commands.Update.Command, User>();
        CreateMap<User, UpdateDto>();
        #endregion

         #region Cv
         CreateMap<CQRS.Cv.Handlers.Commands.CreateCv.CvCommand, Cv>();
         CreateMap<Cv, CreateDtoCv>();
        #endregion


        #region Cv
        CreateMap<CQRS.Cv.Handlers.Commands.UpdateCv.CvCommand, Cv>();
        CreateMap<Cv, UpdateDtoCv>();
        CreateMap<Cv, UpdateDtoCv>().ReverseMap();

        #endregion




    }
}
