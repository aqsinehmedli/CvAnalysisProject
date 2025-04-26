using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands.CreateCv;

namespace CvAnalysisSystem.Application.Services.Concret;

public class ClassicTemplateStrategy : ICvTemplateStrategy
{
    public TemplateType TemplateType => TemplateType.Classic;

    public byte[] Generate(CvCommand command)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Content().Column(col =>
                {
                    // Header – Full Name
                    col.Item().Text(command.FullName)
                        .FontSize(24)
                        .Bold()
                        .FontColor(Colors.Blue.Medium);

                    // Contact Info: Email, Phone, LinkedIn, GitHub
                    col.Item().Text($"{command.Email} | {command.Phone}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken1);
                    col.Item().Text($"LinkedIn: {command.LinkedInUrl} | GitHub: {command.GitHubUrl}")
                        .FontSize(10)
                        .FontColor(Colors.Grey.Darken1);

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    // Education
                    if (command.Educations?.Any() == true)
                    {
                        col.Item().Text("Education")
                            .Bold().FontSize(14).FontColor(Colors.Black);

                        foreach (var edu in command.Educations)
                        {
                            col.Item().PaddingTop(5).Column(eduCol =>
                            {
                                eduCol.Item().Text($"{edu.Degree} in {edu.School}")
                                    .Bold().FontSize(12);
                                eduCol.Item().Text($"{edu.StartYear:yyyy} - {edu.EndYear:yyyy}")
                                    .FontSize(11).FontColor(Colors.Grey.Darken1);
                            });
                        }
                    }

                    // Certificates
                    if (command.Certifications?.Any() == true)
                    {
                        col.Item().PaddingTop(10).Text("Certificates")
                            .Bold().FontSize(14).FontColor(Colors.Black);

                        foreach (var cert in command.Certifications)
                        {
                            col.Item().PaddingTop(5).Column(certCol =>
                            {
                                certCol.Item().Text($"{cert.Title} - {cert.Organization}")
                                    .Bold().FontSize(12);
                                certCol.Item().Text($"Issued: {cert.IssueDate:MMM yyyy} - Expires: {cert.ExpiredDate:MMM yyyy}")
                                    .FontSize(11).FontColor(Colors.Grey.Darken1);
                            });
                        }
                    }

                    // Experience
                    if (command.Experiences?.Any() == true)
                    {
                        col.Item().PaddingTop(10).Text("Experience")
                            .Bold().FontSize(14).FontColor(Colors.Black);

                        foreach (var exp in command.Experiences)
                        {
                            col.Item().PaddingTop(5).Column(expCol =>
                            {
                                expCol.Item().Text($"{exp.Position} at {exp.Company} at {exp.Description}")
                                    .Bold().FontSize(12);
                                expCol.Item().Text($"{exp.StartDate:MMM yyyy} - {(exp.EndDate.HasValue ? exp.EndDate.Value.ToString("MMM yyyy") : "Present")}")
                                    .FontSize(10).FontColor(Colors.Grey.Darken1);
                                expCol.Item().Text(exp.Description).FontSize(11);
                            });
                        }
                    }

                    // Skills
                    if (command.Skills?.Any() == true)
                    {
                        col.Item().PaddingTop(10).Text("Skills")
                            .Bold().FontSize(14).FontColor(Colors.Black);

                        foreach (var skill in command.Skills)
                        {
                            col.Item().Text($"{skill.SkillName} - {skill.ProfiencyLevel}")
                                .FontSize(12);
                        }
                    }
                });
            });
        });

        return document.GeneratePdf();
    }
}

