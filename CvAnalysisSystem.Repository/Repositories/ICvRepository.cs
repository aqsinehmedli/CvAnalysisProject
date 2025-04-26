using CvAnalysisSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Repository.Repositories
{
    public interface ICvRepository
    {
        Task AddAsync(CvModel cvmodel);
        Task Update(CvModel cvmodel);
        Task Delete(int id);
        Task<CvModel?> GetByIdAsync(int id);
        IQueryable<CvModel> GetAll();
        Task RemoveAsync(CvModel cvmodel);
    }
}
