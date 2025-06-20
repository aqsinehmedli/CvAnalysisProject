﻿using CvAnalysisSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CvAnalysisSystem.DAL.SqlServer.Context;

public class AppDbContext : DbContext
{
    public DbSet<EmailVerification> EmailVerifications { get; set; }
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Cv> Cvs { get; set; }
    public DbSet<CvModel> CvModel { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Certifications> Certifications { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }


}
