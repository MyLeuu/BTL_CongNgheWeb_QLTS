using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class BaoTriRepository : IBaoTriRepository
    {
        private readonly QltsContext _context;

        public BaoTriRepository(QltsContext context)
        {
            _context = context;
        }
        public async Task<List<BaoTri>> GetAllAsync()
        {
            return await _context.BaoTris
                .Select(t => new BaoTri
                {
                    MaBaoTri = t.MaBaoTri,
                    MaPhanBo = t.MaPhanBo,
                    NgayBaoTri = t.NgayBaoTri,
                    TrangThai = t.TrangThai,
                    SoLuongBaoTri = t.SoLuongBaoTri,
                    NgayHoanThanh = t.NgayHoanThanh,
                    NguoiThucHien = t.NguoiThucHien
                }).ToListAsync();
        }
        public async Task<BaoTri> GetByIdAsync(int id)
        {
            var baoTri = await _context.BaoTris.FindAsync(id);

            if (baoTri == null)
            {
                return null;
            }

            // Chuyển đổi entity NguoiDung sang NguoiDungDTO
            return baoTri;
            
        }
        public async Task AddAsync(BaoTri baoTri)
        {
            var baoTriNew = new BaoTri
            {
                MaBaoTri = baoTri.MaBaoTri,
                MaPhanBo = baoTri.MaPhanBo,
                NgayBaoTri = baoTri.NgayBaoTri,
                TrangThai = baoTri.TrangThai,
                SoLuongBaoTri = baoTri.SoLuongBaoTri,
                NgayHoanThanh = baoTri.NgayHoanThanh,
                NguoiThucHien = baoTri.NguoiThucHien
            };

            await _context.BaoTris.AddAsync(baoTri);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(BaoTri baoTriDTO)
        {
            var existingBaoTri = await _context.BaoTris.FindAsync(baoTriDTO.MaBaoTri);

            if (existingBaoTri == null)
            {
                // Handle the case where the user does not exist (could throw an exception or return a specific result)
                return;
            }

            // Cập nhật thông tin từ DTO sang entity
            existingBaoTri.MaBaoTri = baoTriDTO.MaBaoTri;
            existingBaoTri.MaPhanBo = baoTriDTO.MaPhanBo;
            existingBaoTri.NgayBaoTri = baoTriDTO.NgayBaoTri;
            existingBaoTri.TrangThai = baoTriDTO.TrangThai;
            existingBaoTri.SoLuongBaoTri = baoTriDTO.SoLuongBaoTri;
            existingBaoTri.NgayHoanThanh = baoTriDTO.NgayHoanThanh;
            existingBaoTri.NguoiThucHien = baoTriDTO.NguoiThucHien;

            _context.BaoTris.Update(existingBaoTri);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int maBaoTri)
        {
            var baoTri = await _context.BaoTris.FindAsync(maBaoTri);

            if (baoTri == null)
            {
                // Nếu không tìm thấy, không có gì để xóa
                throw new KeyNotFoundException($"Không tìm thấy phân bổ tài sản với mã {maBaoTri}.");
            }

            _context.BaoTris.Remove(baoTri);
            await _context.SaveChangesAsync();
        }
    }
}
