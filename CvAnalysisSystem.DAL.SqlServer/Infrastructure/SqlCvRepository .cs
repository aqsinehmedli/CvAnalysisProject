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

        public IQueryable<CvModel> GetAll()
        {
            return _context.CvModel.Where(c => !c.IsDeleted);
        }

        public async Task<CvModel?> GetByIdAsync(int id)
        {
            return await _context.CvModel.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task AddAsync(CvModel cvmodel)
        {
            cvmodel.CreatedDate = DateTime.Now;
            await _context.CvModel.AddAsync(cvmodel);
            await _context.SaveChangesAsync();
        }

        public async Task Update(CvModel cvmodel)
        {
            var existingCv = await _context.CvModel.FirstOrDefaultAsync(c => c.Id == cvmodel.Id && !c.IsDeleted);
            if (existingCv == null)
            {
                throw new NotFoundException("CV not found.");
            }

            existingCv.Educations = cvmodel.Educations;
            existingCv.Experiences = cvmodel.Experiences;
            existingCv.Skills = cvmodel.Skills;
            existingCv.Languages = cvmodel.Languages;
            existingCv.Certifications = cvmodel.Certifications;
            existingCv.UpdatedDate = DateTime.Now;

            _context.CvModel.Update(existingCv);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cv = await _context.CvModel.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (cv == null)
            {
                throw new NotFoundException("CV not found.");
            }

            cv.IsDeleted = true;
            cv.DeletedDate = DateTime.Now;

            _context.CvModel.Update(cv);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(CvModel cvmodel)
        {
            if (cvmodel == null)
            {
                throw new ArgumentNullException(nameof(cvmodel), "CV cannot be null.");
            }

            _context.CvModel.Remove(cvmodel);
            await _context.SaveChangesAsync();
        }

  
    }
}
