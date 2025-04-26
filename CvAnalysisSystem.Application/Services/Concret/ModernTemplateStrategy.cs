using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Domain.Enums;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands.CreateCv;

namespace CvAnalysisSystem.Application.Services.Concret;

public class ModernTemplateStrategy : ICvTemplateStrategy
{
    public TemplateType TemplateType => TemplateType.Modern;

    public byte[] Generate(CvCommand command)
    {   
        QuestPDF.Settings.License = LicenseType.Community;

        // Generate the document with the page content
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(0);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(10));

                page.Content().Row(row =>
                {
                    // Left Column
                    row.ConstantItem(180).Background("#E5F6F7").Padding(20).Column(col =>
                    {
                        col.Item().PaddingTop(20).Text("CONTACT ME AT").Bold().FontColor("#0E8EA8").FontSize(11);
                        //col.Item().PaddingVertical(5).Text($"📍 {command.Address}");
                        col.Item().Text($"📧 {command.Email}");
                        col.Item().Text($"📞 {command.Phone}");
                        //col.Item().Text($"🌐 {command.Website}");

                        col.Item().PaddingTop(20).Text("EDUCATION").Bold().FontColor("#0E8EA8").FontSize(11);
                        foreach (var edu in command.Educations)
                        {
                            col.Item().PaddingVertical(5).Text($"{edu.School}\n{edu.Degree}\n{edu.StartYear} - {edu.EndYear}").FontSize(9);
                        }

                        col.Item().PaddingTop(20).Text("AWARDS RECEIVED").Bold().FontColor("#0E8EA8").FontSize(11);
                        //foreach (var award in command.Awards)
                        {
                            //col.Item().Text(award);
                        }

                        col.Item().PaddingTop(20).Text("PROFESSIONAL SKILLS").Bold().FontColor("#0E8EA8").FontSize(11);
                        //col.Item().Text($"Hard Skills: {string.Join(", ", command.HardSkills)}").FontSize(9);
                        //col.Item().Text($"Soft Skills: {string.Join(", ", command.SoftSkills)}").FontSize(9);
                    });

                    // Right Column
                    row.RelativeItem().Padding(25).Column(col =>
                    {
                        col.Item().Text(command.FullName).Bold().FontColor("#0E8EA8").FontSize(24);
                        //col.Item().Text(command.JobTitle).Bold().FontColor("#000000").FontSize(14);
                        //col.Item().PaddingTop(10).Text(command.ProfileDescription).FontSize(10);

                        col.Item().PaddingTop(20).Text("WORK EXPERIENCE").Bold().FontColor("#0E8EA8").FontSize(11);
                        foreach (var experience in command.Experiences)
                        {
                            col.Item().PaddingVertical(5).Text($"{experience.Position}\n{experience.Company}\n{experience.StartDate:MMM yyyy} - {experience.EndDate:MMM yyyy}")
                                .Bold();
                            col.Item().Text(experience.Description).FontSize(9);
                        }
                    });
                });
            });
        });

        // Return the generated PDF as a byte array
        return document.GeneratePdf();
    }
}
