using CvAnalysisSystem.Common.Exceptions;
using CvAnalysisSystem.DAL.SqlServer.Context;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CvAnalysisSystem.DAL.SqlServer.Infrastructure
{
    public class SqlCvRepository : ICvRepository
    {
        private readonly AppDbContext _context;

        public SqlCvRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Cv> GetAll()
        {
            return _context.Cvs.Where(c => !c.IsDeleted);
        }

        public async Task<Cv?> GetByIdAsync(int id)
        {
            return await _context.Cvs.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task AddAsync(CvModel cvmodel)
        {
            cvmodel.CreatedDate = DateTime.Now;
            await _context.CvModel.AddAsync(cvmodel);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Cv cv)
        {
            var existingCv = await _context.Cvs.FirstOrDefaultAsync(c => c.Id == cv.Id && !c.IsDeleted);
            if (existingCv == null)
            {
                throw new NotFoundException("CV not found.");
            }

            existingCv.PdfFilePath = cv.PdfFilePath;
            existingCv.Education = cv.Education;
            existingCv.WorkExperience = cv.WorkExperience;
            existingCv.Skills = cv.Skills;
            existingCv.Languages = cv.Languages;
            existingCv.Certifications = cv.Certifications;
            existingCv.Status = cv.Status;
            existingCv.LastUpdated = DateTime.Now;
            existingCv.AiAnalysis = cv.AiAnalysis;

            _context.Cvs.Update(existingCv);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cv = await _context.Cvs.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (cv == null)
            {
                throw new NotFoundException("CV not found.");
            }

            cv.IsDeleted = true;
            cv.DeletedDate = DateTime.Now;

            _context.Cvs.Update(cv);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Cv cv)
        {
            if (cv == null)
            {
                throw new ArgumentNullException(nameof(cv), "CV cannot be null.");
            }

            _context.Cvs.Remove(cv);
            await _context.SaveChangesAsync();
        }

    }
}
