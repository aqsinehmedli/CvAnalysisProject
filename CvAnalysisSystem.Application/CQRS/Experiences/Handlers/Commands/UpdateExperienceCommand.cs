using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.Experiences.Commands;

public class UpdateExperienceCommand : IRequest
{
    public int Id { get; set; }
    public string Company { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string UpdatedBy { get; set; } = null!;
    public DateTime UpdatedDate { get; set; }
}
