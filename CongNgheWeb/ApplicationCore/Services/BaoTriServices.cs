using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class BaoTriServices
    {
        private readonly IBaoTriRepository _baoTriRepository;

        public BaoTriServices(IBaoTriRepository baoTriRepository)
        {
            _baoTriRepository = baoTriRepository;
        }
        public async Task<List<BaoTri>> GetAllBaoTri()
        {
            return await _baoTriRepository.GetAllAsync();
        }

        public async Task<BaoTri> GetTBaoTriById(int id)
        {
            return await _baoTriRepository.GetByIdAsync(id);
        }

        public async Task AddBaoTri(BaoTri baotri)
        {
            await _baoTriRepository.AddAsync(baotri);
        }

        public async Task UpdateBaoTri(BaoTri baotri)
        {
            await _baoTriRepository.UpdateAsync(baotri);
        }

        public async Task DeleteBaoTri(int id)
        {
            await _baoTriRepository.DeleteAsync(id);
        }
    }
}
