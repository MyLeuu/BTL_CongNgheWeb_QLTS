using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;


namespace ApplicationCore.Interfaces.Repositories
{
    public interface ITaiSanRepository
    {
        Task<List<TaiSan>> GetAllAsync();

        Task<TaiSan> GetByIdAsync(int id);

        Task AddAsync(TaiSan taiSan);

        Task UpdateAsync(TaiSan taiSan);

        Task DeleteAsync(int id);
    }
}
