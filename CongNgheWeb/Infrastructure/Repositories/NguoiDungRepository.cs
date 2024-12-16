using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Data;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class NguoiDungRepository : INguoiDungRepository
    {
        private readonly QltsContext _context;

        public NguoiDungRepository(QltsContext context)
        {
            _context = context;
        }
        public async Task<List<NguoiDung>> GetAllAsync()
        {
            return await _context.NguoiDungs
                .Select(t => new NguoiDung
                {
                    MaNguoiDung = t.MaNguoiDung,
                    HoTen = t.HoTen,
                    SoDienThoai = t.SoDienThoai,
                    Email = t.Email,
                    VaiTro= t.VaiTro,
                    MatKhau = t.MatKhau
                }).ToListAsync();
        }
        public async Task<NguoiDung> GetByIdAsync(int id)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(id);

            if (nguoiDung == null)
            {
                return null;
            }

            // Chuyển đổi entity NguoiDung sang NguoiDungDTO
            return nguoiDung;
        }

        
        public async Task AddAsync(NguoiDung nguoiDungDTO)
        {
            var nguoiDung = new NguoiDung
            {
                MaNguoiDung = nguoiDungDTO.MaNguoiDung,
                HoTen = nguoiDungDTO.HoTen,
                SoDienThoai = nguoiDungDTO.SoDienThoai,
                Email = nguoiDungDTO.Email,
                VaiTro = nguoiDungDTO.VaiTro,
                MatKhau = nguoiDungDTO.MatKhau
            };

            await _context.NguoiDungs.AddAsync(nguoiDungDTO);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(NguoiDung nguoiDungDTO)
        {
            var existingNguoiDung = await _context.NguoiDungs.FindAsync(nguoiDungDTO.MaNguoiDung);

            if (existingNguoiDung == null)
            {
                // Handle the case where the user does not exist (could throw an exception or return a specific result)
                return;
            }

            // Cập nhật thông tin từ DTO sang entity
            existingNguoiDung.HoTen = nguoiDungDTO.HoTen;
            existingNguoiDung.SoDienThoai = nguoiDungDTO.SoDienThoai;
            existingNguoiDung.Email = nguoiDungDTO.Email;
            existingNguoiDung.VaiTro = nguoiDungDTO.VaiTro;
            existingNguoiDung.MatKhau = nguoiDungDTO.MatKhau;

            _context.NguoiDungs.Update(existingNguoiDung);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int maNguoiDung)
        {
            var nguoiDung = await _context.NguoiDungs.FindAsync(maNguoiDung);

            if (nguoiDung == null)
            {
                // Handle the case where the user does not exist
                return;
            }

            _context.NguoiDungs.Remove(nguoiDung);
            await _context.SaveChangesAsync();
        }
        public async Task<NguoiDung> GetByEmailAndPasswordAsync(string email, string matKhau)
        {
            return await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.Email == email && u.MatKhau == matKhau);
        }

    }
}
