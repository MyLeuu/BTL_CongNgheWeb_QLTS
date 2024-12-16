using ApplicationCore.DTOs;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Data;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LoaiTaiSanRepository:ILoaiTaiSanRepository
    {
        private readonly QltsContext _context;

        public LoaiTaiSanRepository(QltsContext context)
        {
            _context = context;
        }
        public async Task<List<LoaiTaiSan>> GetAllAsync()
        {
            return await _context.LoaiTaiSans
                .Select(t => new LoaiTaiSan
                {
                    MaLoaiTaiSan = t.MaLoaiTaiSan,
                    TenLoaiTaiSan = t.TenLoaiTaiSan,
                    MoTa = t.MoTa,
                }).ToListAsync();
        }
        public async Task<LoaiTaiSan> GetByIdAsync(int id)
        {
            var loaiTS = await _context.LoaiTaiSans.FindAsync(id);

            if (loaiTS == null)
            {
                return null;
            }
           return loaiTS;
        }
        public async Task AddAsync(LoaiTaiSan loaiTaiSan)
        {
            var loaiTSNew = new LoaiTaiSan
            {
                MaLoaiTaiSan = loaiTaiSan.MaLoaiTaiSan,
                TenLoaiTaiSan = loaiTaiSan.TenLoaiTaiSan,
                MoTa = loaiTaiSan.MoTa,
            };

            await _context.LoaiTaiSans.AddAsync(loaiTaiSan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LoaiTaiSan loaiTaiSanDTO)
        {
            var existingLoaiTS = await _context.LoaiTaiSans.FindAsync(loaiTaiSanDTO.MaLoaiTaiSan);

            if (existingLoaiTS == null)
            {
                return;
            }

            // Cập nhật thông tin từ DTO sang entity
            existingLoaiTS.MaLoaiTaiSan = loaiTaiSanDTO.MaLoaiTaiSan;
            existingLoaiTS.TenLoaiTaiSan = loaiTaiSanDTO.TenLoaiTaiSan;
            existingLoaiTS.MoTa = loaiTaiSanDTO.MoTa;

            _context.LoaiTaiSans.Update(existingLoaiTS);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int maLoaiTS)
        {
            var loaiTS = await _context.LoaiTaiSans.FindAsync(maLoaiTS);

            if (loaiTS == null)
            {
                // Nếu không tìm thấy, không có gì để xóa
                throw new KeyNotFoundException($"Không tìm thấy phân bổ tài sản với mã {maLoaiTS}.");
            }

            _context.LoaiTaiSans.Remove(loaiTS);
            await _context.SaveChangesAsync();
        }
    }
}
