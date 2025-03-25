using CvAnalysisSystem.Domain.BaseEntities;
using CvAnalysisSystem.Domain.Enums;
using System.Reflection;

namespace CvAnalysisSystem.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public DateTime BirthDate { get; set; }
    public string MobilePhone { get; set; }
    public string Email { get; set; }
    public string Location { get; set; }
    public string PasswordHash { get; set; }
    public UserRoles? UserRoles { get; set; }
    public Gender? Gender { get; set; }

}
