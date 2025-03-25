namespace CvAnalysisSystem.Domain.Entities;

public class Certification
{
    public int Id { get; set; }
    public int CvId { get; set; }
    public string CertificationName { get; set; }
    public string IssuedBy { get; set; }
    public DateTime IssuedDate { get; set; }
}
