using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class TaiSanServices
    {
        private readonly ITaiSanRepository _taiSanRepository;

        public TaiSanServices(ITaiSanRepository taiSanRepository)
        {
            _taiSanRepository = taiSanRepository;
        }
        public async Task<List<TaiSan>> GetAllTaiSan()
        {
            return await _taiSanRepository.GetAllAsync();
        }

        public async Task<TaiSan> GetTaiSanById(int id)
        {
            return await _taiSanRepository.GetByIdAsync(id);
        }

        public async Task AddTaiSan(TaiSan taisan)
        {
            await _taiSanRepository.AddAsync(taisan);
        }

        public async Task UpdateTaiSan(TaiSan taiSan)
        {
            await _taiSanRepository.UpdateAsync(taiSan);
        }

        public async Task DeleteTaiSan(int id)
        {
            await _taiSanRepository.DeleteAsync(id);
        }
    }
}
