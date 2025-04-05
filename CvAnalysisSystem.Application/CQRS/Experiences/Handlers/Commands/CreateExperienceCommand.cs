using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.Experiences.Commands;

public class CreateExperienceCommand : IRequest<int> 
{
    public int CvModelId { get; set; }
    public string Company { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
