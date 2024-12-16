using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PhongRepository:IPhongRepository
    {
        private readonly QltsContext _context;

        public PhongRepository(QltsContext context)
        {
            _context = context;
        }
        public async Task<List<Phong>> GetAllAsync()
        {
            return await _context.Phongs
                .Select(t => new Phong
                {
                    MaPhong = t.MaPhong,
                    ViTri = t.ViTri,
                    TenPhong = t.TenPhong,
                }).ToListAsync();
        }
        public async Task<Phong> GetByIdAsync(int id)
        {
            var phong = await _context.Phongs.FindAsync(id);

            if (phong == null)
            {
                return null;
            }

            return phong;
        }
        public async Task AddAsync(Phong phongDTO)
        {
            var phong = new Phong
            {
                MaPhong = phongDTO.MaPhong,
                ViTri = phongDTO.ViTri,
                TenPhong = phongDTO.TenPhong,
            };

            await _context.Phongs.AddAsync(phongDTO);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Phong phongDTO)
        {
            var existingPhong = await _context.Phongs.FindAsync(phongDTO.MaPhong);

            if (existingPhong == null)
            {
                // Handle the case where the user does not exist (could throw an exception or return a specific result)
                return;
            }

            // Cập nhật thông tin từ DTO sang entity
            existingPhong.MaPhong = phongDTO.MaPhong;
            existingPhong.ViTri = phongDTO.ViTri;
            existingPhong.TenPhong = phongDTO.TenPhong;

            _context.Phongs.Update(existingPhong);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int maPhong)
        {
            var phong = await _context.Phongs.FindAsync(maPhong);

            if (phong == null)
            {
                // Nếu không tìm thấy, không có gì để xóa
                throw new KeyNotFoundException($"Không tìm thấy phân bổ tài sản với mã {maPhong}.");
            }

            _context.Phongs.Remove(phong);
            await _context.SaveChangesAsync();
        }
    }
}
