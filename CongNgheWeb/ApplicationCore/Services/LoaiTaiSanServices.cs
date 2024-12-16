using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class LoaiTaiSanServices
    {
        private readonly ILoaiTaiSanRepository _loaiTaiSanRepository;

        public LoaiTaiSanServices(ILoaiTaiSanRepository loaiTaiSanRepository)
        {
            _loaiTaiSanRepository = loaiTaiSanRepository;
        }
        public async Task<List<LoaiTaiSan>> GetAllLoaiTS()
        {
            return await _loaiTaiSanRepository.GetAllAsync();
        }

        public async Task<LoaiTaiSan> GetTLoaiTSById(int id)
        {
            return await _loaiTaiSanRepository.GetByIdAsync(id);
        }

        public async Task AddLoaiTS(LoaiTaiSan loaiTaiSan)
        {
            await _loaiTaiSanRepository.AddAsync(loaiTaiSan);
        }

        public async Task UpdateLoaiTS(LoaiTaiSan loaiTaiSan)
        {
            await _loaiTaiSanRepository.UpdateAsync(loaiTaiSan);
        }

        public async Task DeleteLoaiTS(int id)
        {
            await _loaiTaiSanRepository.DeleteAsync(id);
        }
    }
}
