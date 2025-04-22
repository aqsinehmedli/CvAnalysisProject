using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Domain.Enums;
using static CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands.CreateCv;

namespace CvAnalysisSystem.Application.Services.Concret;

public class CvService(IEnumerable<ICvTemplateStrategy> templates, ClassicTemplateStrategy classicTemplateStrategy, ModernTemplateStrategy modernTemplateStrategy)
{
    private readonly ClassicTemplateStrategy _classicTemplateStrategy= classicTemplateStrategy;
    private readonly ModernTemplateStrategy _modernTemplateStrategy=modernTemplateStrategy;
    public byte[] GenerateCv(CvCommand command, TemplateType templateType)
    {
        ICvTemplateStrategy selectedStrategy;

        // TemplateType-a əsasən doğru strategiya seçilir
        switch (templateType)
        {
            case TemplateType.Classic:
                selectedStrategy = _classicTemplateStrategy;
                break;

            case TemplateType.Modern:
                selectedStrategy = _modernTemplateStrategy;
                break;

            default:
                throw new InvalidOperationException("Unknown template type.");
        }

        // Seçilən strategiya ilə CV yaradılır
        return selectedStrategy.Generate(command);
    }

}
