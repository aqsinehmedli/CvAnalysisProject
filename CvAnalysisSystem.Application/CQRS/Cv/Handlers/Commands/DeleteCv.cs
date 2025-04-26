    using CvAnalysisSystem.Common.Exceptions;
    using CvAnalysisSystem.Repository.Common;
    using MediatR;

    public class DeleteCv
    {
        public record struct CvCommand : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<CvCommand>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(CvCommand request, CancellationToken cancellationToken)
            {
                var cv = await _unitOfWork.CvRepository.GetByIdAsync(request.Id);
                if (cv == null)
                {
                    throw new NotFoundException("CV not found");
                }

                await _unitOfWork.CvRepository.RemoveAsync(cv);
                await _unitOfWork.SaveChange();

                return Unit.Value; 
            }
        }
    }