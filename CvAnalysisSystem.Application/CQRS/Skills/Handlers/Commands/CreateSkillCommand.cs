using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.Skills.Commands;

public class CreateSkillCommand : IRequest<int> 
{
    public int CvModelId { get; set; }
    public string SkillName { get; set; } = null!;
    public string ProficiencyLevel { get; set; } = null!;
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
