using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IPhongRepository
    {
        Task<List<Phong>> GetAllAsync();

        Task<Phong> GetByIdAsync(int id);

        Task AddAsync(Phong phong);

        Task UpdateAsync(Phong phong);

        Task DeleteAsync(int id);
    }
}
