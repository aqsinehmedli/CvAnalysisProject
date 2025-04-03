using AutoMapper;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Repository.Common;
using MediatR;


namespace CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands
{
    public class CreateCv
    {
        public record struct CvCommand : IRequest<CreateDtoCv>
        {
            public CvCommand()
            {
            }

            public int UserId { get; set; }
            public string PdfFilePath { get; set; }
            public string Education { get; set; }
            public string WorkExperience { get; set; }
            public string Skills { get; set; }
            public string Languages { get; set; }
            public string Certifications { get; set; }
            public string Status { get; set; }
            public string AiAnalysis { get; set; }
            public DateTime DeletedDate { get; set; }
            public bool IsDeleted { get; set; } = false;
        }

        public sealed class Handler : IRequestHandler<CvCommand, CreateDtoCv>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CreateDtoCv> Handle(CvCommand request, CancellationToken cancellationToken)
            {
                var cv = _mapper.Map<CvAnalysisSystem.Domain.Entities.Cv>(request);
                await _unitOfWork.CvRepository.AddAsync(cv);
                await _unitOfWork.SaveChange();
                return _mapper.Map<CreateDtoCv>(cv);
            }
        }
    }

}
