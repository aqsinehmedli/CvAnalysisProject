using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using CvAnalysisSystem.Application.CQRS.Users.DTOs;
using CvAnalysisSystem.Domain.Entities;
using static CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands.CreateCv;
using static CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.Register;

namespace CvAnalysisSystem.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region User
        CreateMap<Command, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        CreateMap<User, RegisterDto>();
        #endregion


        #region Update
        CreateMap<CQRS.Users.Handlers.Commands.Update.Command, User>();
        CreateMap<User, UpdateDto>();
        #endregion

        #region Cv

        // Education
        CreateMap<EducationDto, Education>()
            .ForMember(dest => dest.StartYear, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartYear, DateTimeKind.Utc)))
            .ForMember(dest => dest.EndYear, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndYear, DateTimeKind.Utc)))
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // BaseEntity fields

            .ReverseMap();

        // Experience
        CreateMap<ExperienceDto, Experience>()
        //    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartDate, DateTimeKind.Utc)))
        //    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.EndDate, DateTimeKind.Utc)))
        //    .ForMember(dest => dest.Id, opt => opt.Ignore())

        //    .ReverseMap();
        .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src =>
    src.StartDate.HasValue ? DateTime.SpecifyKind(src.StartDate.Value, DateTimeKind.Utc) : (DateTime?)null))

.ForMember(dest => dest.EndDate, opt => opt.MapFrom(src =>
    src.EndDate.HasValue ? DateTime.SpecifyKind(src.EndDate.Value, DateTimeKind.Utc) : (DateTime?)null));


        // Skill
        CreateMap<SkillDto, Skill>()
            .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.SkillName))
            .ForMember(dest => dest.ProficiencyLevel, opt => opt.MapFrom(src => src.ProfiencyLevel))
            .ForMember(dest => dest.Id, opt => opt.Ignore())

            .ReverseMap();
        CreateMap<LanguageDto, Language>()
            .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.LanguageName))
            .ForMember(dest => dest.FluencyLevel, opt => opt.MapFrom(src => src.FluencyLevel))
            .ForMember(dest => dest.Id, opt => opt.Ignore())

            .ReverseMap();
        // Certification
        CreateMap<CertificationDto, Certifications>()
            .ForMember(dest => dest.IssueDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.IssueDate, DateTimeKind.Utc)))
            .ForMember(dest => dest.Id, opt => opt.Ignore())

            .ReverseMap();

        // CvCommand → CvModel
        CreateMap<CvCommand, CvModel>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.LinkedInUrl, opt => opt.MapFrom(src => src.LinkedInUrl))
            .ForMember(dest => dest.GitHubUrl, opt => opt.MapFrom(src => src.GitHubUrl))
            .ForMember(dest => dest.TemplateType, opt => opt.MapFrom(src => src.TemplateType))
            .ForMember(dest => dest.Educations, opt => opt.MapFrom(src => src.Educations))
            .ForMember(dest => dest.Experiences, opt => opt.MapFrom(src => src.Experiences))
            .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
            .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
            .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DeletedBy, opt => opt.Ignore())
            .ReverseMap();

        // CvModel ↔ CreateCvDto
        CreateMap<CvModel, CreateCvDto>()
            .ReverseMap()
             .ForMember(dest => dest.Id, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
    .ForMember(dest => dest.UpdatedDate, opt => opt.Ignore())
    .ForMember(dest => dest.DeletedDate, opt => opt.Ignore())
    .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
    .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
    .ForMember(dest => dest.DeletedBy, opt => opt.Ignore());

        #endregion

        #region Cv
        CreateMap<CvModel, UpdateDtoCv>()
          .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
          .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
          .ForMember(dest => dest.LinkedInUrl, opt => opt.MapFrom(src => src.LinkedInUrl))
          .ForMember(dest => dest.GitHubUrl, opt => opt.MapFrom(src => src.GitHubUrl))
          .ForMember(dest => dest.TemplateId, opt => opt.MapFrom(src => (int)src.TemplateType)) // enum maping
          .ForMember(dest => dest.Educations, opt => opt.MapFrom(src => src.Educations))
          .ForMember(dest => dest.Experiences, opt => opt.MapFrom(src => src.Experiences))
          .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills))
          .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications))
          .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages));
        CreateMap<CQRS.Cv.Handlers.Commands.UpdateCv.CvCommand, CvModel>();
    }


    #endregion




}