using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.Education.Commands;

public class CreateEducationCommand : IRequest<int> { 
    public int CvModelId { get; set; }
    public string School { get; set; } = null!;
    public string Degree { get; set; } = null!;
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
