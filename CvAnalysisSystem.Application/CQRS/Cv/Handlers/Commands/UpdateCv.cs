using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Common.Exceptions;
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
            public int Id { get; set; }
            public string PdfFilePath { get; set; }
            public string Education { get; set; }
            public string WorkExperience { get; set; }
            public string Skills { get; set; }
            public string Languages { get; set; }
            public string Certifications { get; set; }
            public string Status { get; set; }
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

            public async Task<UpdateDtoCv  > Handle(CvCommand request, CancellationToken cancellationToken)
            {
                var cv = await _unitOfWork.CvRepository.GetByIdAsync(request.Id);
                Console.WriteLine(request.Id);
                if (cv == null) { throw new NotFoundException("CV not found"); }
                cv.PdfFilePath = request.PdfFilePath;
                cv.Education = request.Education;
                cv.WorkExperience = request.WorkExperience;
                cv.Skills = request.Skills;
                cv.Languages = request.Languages;
                cv.Certifications = request.Certifications;
                cv.Status = request.Status;

                await _unitOfWork.CvRepository.Update(cv);
                await _unitOfWork.SaveChange();

                return _mapper.Map<UpdateDtoCv>(cv);
            }
        }
    }
}
