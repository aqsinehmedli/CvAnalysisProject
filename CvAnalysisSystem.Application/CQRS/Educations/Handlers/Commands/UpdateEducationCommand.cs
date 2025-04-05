using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.Education.Commands;

public class UpdateEducationCommand : IRequest
{
    public int Id { get; set; }
    public string School { get; set; } = null!;
    public string Degree { get; set; } = null!;
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string UpdatedBy { get; set; } = null!;
    public DateTime UpdatedDate { get; set; }
}
