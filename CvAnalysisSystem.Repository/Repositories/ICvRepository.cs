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
        Task Update(Cv cv);
        Task Delete(int id);
        Task<Cv?> GetByIdAsync(int id);
        IQueryable<Cv> GetAll();
        Task RemoveAsync(Cv cv);
    }
}
