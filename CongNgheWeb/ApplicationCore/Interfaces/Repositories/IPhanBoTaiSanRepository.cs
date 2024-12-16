using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IPhanBoTaiSanRepository
    {
        Task<List<PhanBoTaiSan>> GetAllAsync();

        Task<PhanBoTaiSan> GetByIdAsync(int id);

        Task AddAsync(PhanBoTaiSan phanBoTaiSan);

        Task UpdateAsync(PhanBoTaiSan phanBoTaiSan);

        Task DeleteAsync(int id);
    }
}
