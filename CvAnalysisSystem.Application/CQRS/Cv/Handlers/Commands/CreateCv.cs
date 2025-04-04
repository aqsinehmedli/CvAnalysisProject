using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Repository.Common;
using MediatR;


namespace CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands
{
    public class CreateCv
    {
        public record struct CvCommand : IRequest<CreateCvDto>
        {
            public CvCommand()
            {
            }
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string LinkedInUrl { get; set; }
            public string GitHubUrl { get; set; }
            public string TemplateName { get; set; }

            public List<EducationDto> Educations { get; set; }
            public List<ExperienceDto> Experiences { get; set; }
            public List<SkillDto> Skills { get; set; }
            public List<CertificationDto> Certifications { get; set; }
        }

        public sealed class Handler : IRequestHandler<CvCommand, CreateCvDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CreateCvDto> Handle(CvCommand request, CancellationToken cancellationToken)
            {
                var cvModel = _mapper.Map<CvModel>(request);
                await _unitOfWork.CvRepository.AddAsync(cvModel);
                await _unitOfWork.SaveChange();
                return _mapper.Map<CreateCvDto>(cvModel);
            }
        }
    }

}
