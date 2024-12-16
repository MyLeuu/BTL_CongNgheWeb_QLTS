using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;


namespace ApplicationCore.Interfaces.Repositories
{
    public interface IBaoTriRepository
    {
        Task<List<BaoTri>> GetAllAsync();

        Task<BaoTri> GetByIdAsync(int id);

        Task AddAsync(BaoTri baoTri);

        Task UpdateAsync(BaoTri baoTri);

        Task DeleteAsync(int id);
    }
}
