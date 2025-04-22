using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CvAnalysisSystem.Application.Services.Concret;

public class ModernTemplateStrategy : ICvTemplateStrategy
{
    public TemplateType TemplateType => TemplateType.Modern;

    public byte[] Generate(CreateCv.CvCommand command)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(12));

                // Header Section: Name and Contact Info
                page.Header().Row(row =>
                {
                    row.RelativeColumn().Column(col =>
                    {
                        col.Item().Text(command.FullName)
                            .FontSize(32).Bold().FontColor(Colors.Black);

                        col.Item().Text(command.Email).FontColor(Colors.Grey.Darken1);
                        col.Item().Text(command.Phone).FontColor(Colors.Grey.Darken1);
                    });

                    row.ConstantColumn(180).Column(col =>
                    {
                        col.Item().AlignRight().Text($"LinkedIn:").Bold();
                        col.Item().AlignRight().Text(command.LinkedInUrl).FontColor(Colors.Blue.Darken2);

                        col.Item().AlignRight().Text($"GitHub:").Bold();
                        col.Item().AlignRight().Text(command.GitHubUrl).FontColor(Colors.Blue.Darken2);
                    });
                });

                // Main Content Section
                page.Content().PaddingVertical(20).Column(col =>
                {
                    // Education Section
                    if (command.Educations?.Any() == true)
                    {
                        col.Item().Text("🎓 Education").Bold().FontSize(14).FontColor(Colors.Black);
                        foreach (var edu in command.Educations)
                        {
                            col.Item().PaddingVertical(5).Row(row =>
                            {
                                row.RelativeColumn().Text($"{edu.School}").Bold();
                                row.ConstantColumn(120).AlignRight().Text($"{edu.StartYear:yyyy} - {edu.EndYear:yyyy}").FontColor(Colors.Grey.Darken1);
                            });

                            col.Item().Text($"Degree: {edu.Degree}").FontColor(Colors.Grey.Darken2).FontSize(11);
                        }
                    }

                    // Experience Section
                    if (command.Experiences?.Any() == true)
                    {
                        col.Item().PaddingTop(15).Text("💼 Experience").Bold().FontSize(14).FontColor(Colors.Black);

                        foreach (var exp in command.Experiences)
                        {
                            col.Item().PaddingVertical(5).Column(expCol =>
                            {
                                expCol.Item().Text($"{exp.Position} at {exp.Company}")
                                    .FontSize(12).Bold();

                                expCol.Item().Text($"{exp.StartDate:MMM yyyy} - {exp.EndDate:MMM yyyy}")
                                    .FontSize(10).FontColor(Colors.Grey.Darken1);

                                expCol.Item().Text(exp.Description)
                                    .FontSize(11).FontColor(Colors.Grey.Darken2);
                            });
                        }
                    }

                    // Skills Section (No WrapHorizontal, just clear lists)
                    if (command.Skills?.Any() == true)
                    {
                        col.Item().PaddingTop(15).Text("🧠 Skills").Bold().FontSize(14).FontColor(Colors.Black);

                        col.Item().Column(skillCol =>
                        {
                            foreach (var skill in command.Skills)
                            {
                                skillCol.Item().Text($"{skill.SkillName} - Level {skill.ProfiencyLevel}")
                                    .FontSize(12)
                                    .FontColor(Colors.Grey.Darken1);
                            }
                        });
                    }

                    // Certifications Section
                    if (command.Certifications?.Any() == true)
                    {
                        col.Item().PaddingTop(15).Text("📜 Certifications").Bold().FontSize(14).FontColor(Colors.Black);

                        foreach (var cert in command.Certifications)
                        {
                            col.Item().PaddingVertical(5).Column(certCol =>
                            {
                                certCol.Item().Text($"{cert.Title} - {cert.Organization}")
                                    .FontSize(12).Bold();

                                certCol.Item().Text($"Issued: {cert.IssueDate:MMM yyyy} - Expires: {cert.ExpiredDate:MMM yyyy}")
                                    .FontSize(10).FontColor(Colors.Grey.Darken1);
                            });
                        }
                    }
                });

                // Footer Section with Contact Info
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Generated with ").FontColor(Colors.Grey.Lighten1);
                    txt.Span("CVAnalysisSystem").SemiBold().FontColor(Colors.Blue.Medium);
                });
            });
        });

        return document.GeneratePdf();
    }
}
