using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.CvModels.Commands;

public class UpdateCvModelCommand : IRequest
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? LinkedInUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string? TemplateName { get; set; }
    public string UpdatedBy { get; set; } = null!;
    public DateTime UpdatedDate { get; set; }
}
