using MediatR;
using System;

namespace CvAnalysisSystem.Application.CQRS.CvModels.Commands;

public class CreateCvModelCommand : IRequest<int> 
{
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? LinkedInUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string? TemplateName { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
