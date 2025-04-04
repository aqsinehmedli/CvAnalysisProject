using System.ComponentModel.DataAnnotations.Schema;

namespace CvAnalysisSystem.Domain.Entities;

public class Experience
{
    public int Id { get; set; }

    public int CvModelId { get; set; } // Foreign key
    public string Company { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [ForeignKey("CvModelId")]
    public CvModel CvModel { get; set; }
}
