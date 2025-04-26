using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Domain.Enums;

namespace CvAnalysisSystem.Application.Services.Concret;

public class CvTemplateResolver(IEnumerable<ICvTemplateStrategy> templates):ICvTemplateStrategyResolver
{
    private readonly IEnumerable<ICvTemplateStrategy> _templates = templates;
    public ICvTemplateStrategy Resolve(TemplateType templateType)
    {
        var strategy = _templates.FirstOrDefault(s => s.TemplateType == templateType);
        if (strategy == null)
            throw new Exception($"Template strategy for '{templateType}' not found.");
        return strategy;
    }
}

