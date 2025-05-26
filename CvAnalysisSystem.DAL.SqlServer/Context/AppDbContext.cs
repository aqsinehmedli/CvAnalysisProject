using CvAnalysisSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CvAnalysisSystem.DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet <Cv> Cvs { get;  set; }
    public DbSet<CvModel> CvModel { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Skill> Skills { get; set; }    
    public DbSet<Experience> Experiences { get; set; }  
    public DbSet<Certifications> Certifications { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public object EmailVerifications { get; internal set; }
}

