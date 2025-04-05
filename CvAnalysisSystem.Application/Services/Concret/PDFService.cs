using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CvAnalysisSystem.Application.Services.Concret
{
    public class PDFService : IPDFService
    {
        public byte[] GenerateCvPdf(CvModel model)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(210,297);  

                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(10);

                            column.Item().Text($"Full Name: {model.FullName}")
                                         .FontSize(20)
                                         .Bold();

                            column.Item().Text($"Email: {model.Email}");
                            column.Item().Text($"Phone: {model.Phone}");
                            column.Item().Text($"LinkedIn: {model.LinkedInUrl}");
                            column.Item().Text($"GitHub: {model.GitHubUrl}");

                            column.Item().Text("Educations:");
                            foreach (var education in model.Educations)
                            {
                                column.Item().Text($"- {education.StartYear} - {education.EndYear}: {education.School}, {education.Degree}");
                            }

                            column.Item().Text("Experiences:");
                            foreach (var experience in model.Experiences)
                            {
                                column.Item().Text($"- {experience.StartDate} - {experience.EndDate}: {experience.Company}, {experience.Position}");
                            }

                            column.Item().Text("Certifications:");
                            foreach (var certification in model.Certifications)
                            {
                                column.Item().Text($"- {certification.IssueDate}: {certification.Organization}");
                            }
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
