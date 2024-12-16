using ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface ILoaiTaiSanRepository
    {
        Task<List<LoaiTaiSan>> GetAllAsync();

        Task<LoaiTaiSan> GetByIdAsync(int id);

        Task AddAsync(LoaiTaiSan loaiTS);

        Task UpdateAsync(LoaiTaiSan loaiTS);

        Task DeleteAsync(int id);
    }
}
