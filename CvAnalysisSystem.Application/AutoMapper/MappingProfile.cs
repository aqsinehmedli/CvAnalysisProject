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
        CreateMap<EducationDto, Education>()
      .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => DateTime.Parse(src.StartYear.ToString())))
      .ForMember(dest => dest.EndYear, opt => opt.MapFrom(src => DateTime.Parse(src.EndYear.ToString())));

        CreateMap<ExperienceDto, Experience>()
     .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.Parse(src.StartDate.ToString())))
     .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTime.Parse(src.EndDate.ToString())));

        CreateMap<CertificationDto, Certification>()
   .ForMember(dest => dest.IssuedDate, opt => opt.MapFrom(src => DateTime.Parse(src.IssueDate.ToString())));

        CreateMap<SkillDto, Skill>()
       .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.SkillName))
       .ForMember(dest => dest.ProficiencyLevel, opt => opt.MapFrom(src => src.ProfiencyLevel));

        CreateMap<CQRS.Cv.Handlers.Commands.CreateCv.CvCommand, CvModel>()
       .ForMember(dest => dest.Educations, opt => opt.MapFrom(src => src.Educations))
       .ForMember(dest => dest.Experiences, opt => opt.MapFrom(src => src.Experiences))
       .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
       .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications));
        //CreateMap<CvModel, CreateCvDto>();
        //CreateMap<SkillDto, Skill>().ReverseMap();
        //CreateMap<EducationDto, Education>().ReverseMap();
        //CreateMap<ExperienceDto, Experience>().ReverseMap();
        //CreateMap<CertificationDto, Certification>().ReverseMap();
       
        #endregion


        #region Cv
        CreateMap<CQRS.Cv.Handlers.Commands.UpdateCv.CvCommand, Cv>();
        CreateMap<Cv, UpdateDtoCv>();
        CreateMap<Cv, UpdateDtoCv>().ReverseMap();

        #endregion




    }
}
