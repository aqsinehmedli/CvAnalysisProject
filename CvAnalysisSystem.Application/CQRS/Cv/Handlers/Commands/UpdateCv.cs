using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Common.Exceptions;
using CvAnalysisSystem.Domain.Enums;
using CvAnalysisSystem.Repository.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands
{
    public class UpdateCv
    {
        public record struct CvCommand : IRequest<UpdateDtoCv>
        {
            public int CvModelId { get; set; }
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

        public class Handler : IRequestHandler<CvCommand, UpdateDtoCv>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<UpdateDtoCv> Handle(CvCommand request, CancellationToken cancellationToken)
            {
                var cv = await _unitOfWork.CvRepository.GetByIdAsync(request.CvModelId);
                Console.WriteLine(request.CvModelId);
                if (cv == null) { throw new NotFoundException("CV not found"); }
                //cv.Educations = request.Educations;
                //cv.Experiences = request.Experiences;
                //cv.Skills = request.Skills;
                //cv.Languages = request.Languages;
                //cv.Certifications = request.Certifications;

                await _unitOfWork.CvRepository.Update(cv);
                await _unitOfWork.SaveChange();

                return _mapper.Map<UpdateDtoCv>(cv);
            }
        }
    }
}
