using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Repository.Common;
using CvAnalysisSystem.Domain.Enums;
using MediatR;
using CvAnalysisSystem.Application.Services.Abstract;


namespace CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands
{
    public class CreateCv
    {
        public record struct CvCommand : IRequest<byte[]>
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
            public TemplateType TemplateType { get; set; }
            public List<EducationDto> Educations { get; set; }
            public List<ExperienceDto> Experiences { get; set; }
            public List<SkillDto> Skills { get; set; }
            public List<CertificationDto> Certifications { get; set; }
            public List<LanguageDto> Languages { get; set; }
        }

        public sealed class Handler : IRequestHandler<CvCommand, byte[]>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly ICvTemplateStrategyResolver _resolver;


            public Handler(IUnitOfWork unitOfWork, IMapper mapper, ICvTemplateStrategyResolver resolver)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _resolver = resolver;
            }

            public async Task<byte[]> Handle(CvCommand request, CancellationToken cancellationToken)
            {
                var cvModel = _mapper.Map<CvModel>(request);
                await _unitOfWork.CvRepository.AddAsync(cvModel);
                await _unitOfWork.SaveChange();
                var strategy = _resolver.Resolve(request.TemplateType);
                var pdfBytes = strategy.Generate(request);
                return pdfBytes;
            }
        }
    }
}
