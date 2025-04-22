using CvAnalysisSystem.Domain.Enums;
using static CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands.CreateCv;

namespace CvAnalysisSystem.Application.Services.Abstract;

public interface ICvTemplateStrategy
{
    byte[] Generate(CvCommand command);
    TemplateType TemplateType {  get; }
}
