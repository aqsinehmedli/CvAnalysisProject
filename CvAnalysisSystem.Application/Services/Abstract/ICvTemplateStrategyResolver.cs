using CvAnalysisSystem.Domain.Enums;

namespace CvAnalysisSystem.Application.Services.Abstract;

public interface ICvTemplateStrategyResolver
{
    ICvTemplateStrategy Resolve(TemplateType templateType);
}
