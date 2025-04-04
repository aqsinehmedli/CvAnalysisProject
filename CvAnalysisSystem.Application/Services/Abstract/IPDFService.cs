using CvAnalysisSystem.Domain.Entities;

namespace CvAnalysisSystem.Application.Services.Abstract;

public interface IPDFService
{
    byte[] GenerateCvPdf(CvModel model);
}
