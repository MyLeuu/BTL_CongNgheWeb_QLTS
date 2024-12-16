using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PhanBoTaiSanServices
    {
        private readonly IPhanBoTaiSanRepository _phanBoTaiSanRepository ;

        public PhanBoTaiSanServices(IPhanBoTaiSanRepository _hanBoTaiSanRepository)
        {
            _phanBoTaiSanRepository = _hanBoTaiSanRepository;
        }
        public async Task<List<PhanBoTaiSan>> GetAllPhanBoTS()
        {
            return await _phanBoTaiSanRepository.GetAllAsync();
        }

        public async Task<PhanBoTaiSan> GetPhanBoTSById(int id)
        {
            return await _phanBoTaiSanRepository.GetByIdAsync(id);
        }

        public async Task AddPhanBoTS(PhanBoTaiSan phanBoTaiSan)
        {
            await _phanBoTaiSanRepository.AddAsync(phanBoTaiSan);
        }

        public async Task UpdatePhanBoTS(PhanBoTaiSan phanBoTaiSan)
        {
            await _phanBoTaiSanRepository.UpdateAsync(phanBoTaiSan);
        }

        public async Task DeletePhanBoTS(int id)
        {
            await _phanBoTaiSanRepository.DeleteAsync(id);
        }
    }
}
