using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.Skills.Commands;

public class UpdateSkillCommand : IRequest
{
    public int Id { get; set; }
    public string SkillName { get; set; } = null!;
    public string ProficiencyLevel { get; set; } = null!;
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string UpdatedBy { get; set; } = null!;
    public DateTime UpdatedDate { get; set; }
}
