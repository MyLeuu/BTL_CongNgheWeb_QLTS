using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class PhongServices
    {
        private readonly IPhongRepository _phongRepository;

        public PhongServices(IPhongRepository phongRepository)
        {
            _phongRepository = phongRepository;
        }
        public async Task<List<Phong>> GetAllPhong()
        {
            return await _phongRepository.GetAllAsync();
        }

        public async Task<Phong> GetPhongById(int id)
        {
            return await _phongRepository.GetByIdAsync(id);
        }

        public async Task AddPhong(Phong phong)
        {
            await _phongRepository.AddAsync(phong);
        }

        public async Task UpdatePhong(Phong phong)
        {
            await _phongRepository.UpdateAsync(phong);
        }

        public async Task DeletePhong(int id)
        {
            await _phongRepository.DeleteAsync(id);
        }
    }
}
