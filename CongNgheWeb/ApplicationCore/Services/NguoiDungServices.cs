using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class NguoiDungServices
    {
        private readonly INguoiDungRepository _nguoiDungRepository;

        public NguoiDungServices(INguoiDungRepository nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
        }
        public async Task<List<NguoiDung>> GetAllNguoiDung()
        {
            return await _nguoiDungRepository.GetAllAsync();
        }

        public async Task<NguoiDung> GetNguouDungById(int id)
        {
            return await _nguoiDungRepository.GetByIdAsync(id);
        }

        public async Task AddNguoiDung(NguoiDung nguoiDung)
        {
            await _nguoiDungRepository.AddAsync(nguoiDung);
        }

        public async Task UpdateNguoiDung(NguoiDung nguoiDung)
        {
            await _nguoiDungRepository.UpdateAsync(nguoiDung);
        }

        public async Task DeleteNguoiDung(int id)
        {
            await _nguoiDungRepository.DeleteAsync(id);
        }
        public async Task<NguoiDung> ValidateUserAsync(string email, string matKhau)
        {
            // Lấy người dùng từ repository dựa trên Email và MatKhau
            var user = await _nguoiDungRepository.GetByEmailAndPasswordAsync(email, matKhau);
            return user;
        }

    }
}
