using System.ComponentModel.DataAnnotations.Schema;

namespace CvAnalysisSystem.Domain.Entities;

public class Education
{
    public int Id { get; set; }
    public int CvModelId { get; set; }
    public string School { get; set; }
    public string Degree { get; set; }
    public DateTime StartYear { get; set; }
    public DateTime EndYear { get; set; }

    [ForeignKey("CvModelId")]
    public CvModel CvModel { get; set; }
}
