namespace CvAnalysisSystem.Application.CQRS.Users.DTOs;

public record struct RegisterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public string MobilePhone { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public int? UserRoles { get; set; }
    public int? Gender { get; set; }
    public DateTime? BirthDate { get; set; }
}
