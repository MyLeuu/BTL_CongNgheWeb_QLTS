using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TaiSanRepository:ITaiSanRepository
    {
        private readonly QltsContext _context;

        public TaiSanRepository(QltsContext context)
        {
            _context = context;
        }
        public async Task<List<TaiSan>> GetAllAsync()
        {
            return await _context.TaiSans
                .Select(t => new TaiSan
                {
                   MaTaiSan = t.MaTaiSan,
                   TenTaiSan = t.TenTaiSan,
                   MaLoaiTaiSan = t.MaLoaiTaiSan,
                   NgayMua = t.NgayMua,
                   GiaTri = t.GiaTri,
                   SoLuong = t.SoLuong,
                   TinhTrang = t.TinhTrang,
                   HinhAnh = t.HinhAnh,
                   MaQr = t.MaQr,
                   ThoiGianBaoHanh = t.ThoiGianBaoHanh
                }).ToListAsync();
        }
        public async Task<TaiSan> GetByIdAsync(int id)
        {
            var taiSan = await _context.TaiSans.FindAsync(id);

            if (taiSan == null)
            {
                return null;
            }

            return taiSan;
        }
        public async Task AddAsync(TaiSan taiSan)
        {
            var taiSanNew = new TaiSan
            {
                MaTaiSan = taiSan.MaTaiSan,
                TenTaiSan = taiSan.TenTaiSan,
                MaLoaiTaiSan = taiSan.MaLoaiTaiSan,
                NgayMua = taiSan.NgayMua,
                GiaTri = taiSan.GiaTri,
                SoLuong = taiSan.SoLuong,
                TinhTrang = taiSan.TinhTrang,
                HinhAnh = taiSan.HinhAnh,
                MaQr = taiSan.MaQr,
                ThoiGianBaoHanh = taiSan.ThoiGianBaoHanh
            };

            await _context.TaiSans.AddAsync(taiSan);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TaiSan taiSanDTO)
        {
            var existingTaiSan = await _context.TaiSans.FindAsync(taiSanDTO.MaTaiSan);

            if (existingTaiSan == null)
            {
                throw new KeyNotFoundException($"Asset with ID {taiSanDTO.MaTaiSan} not found.");
            }

            // Cập nhật thông tin từ DTO sang entity
            existingTaiSan.TenTaiSan = taiSanDTO.TenTaiSan;
            existingTaiSan.MaLoaiTaiSan = taiSanDTO.MaLoaiTaiSan;
            existingTaiSan.NgayMua = taiSanDTO.NgayMua;
            existingTaiSan.GiaTri = taiSanDTO.GiaTri;
            existingTaiSan.SoLuong = taiSanDTO.SoLuong;
            existingTaiSan.TinhTrang = taiSanDTO.TinhTrang;
            existingTaiSan.HinhAnh = taiSanDTO.HinhAnh;
            existingTaiSan.MaQr = taiSanDTO.MaQr;
            existingTaiSan.ThoiGianBaoHanh = taiSanDTO.ThoiGianBaoHanh;

            // Không cần gọi _context.TaiSans.Update()
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int maTaiSan)
        {
            var taiSan = await _context.TaiSans.FindAsync(maTaiSan);

            if (taiSan == null)
            {
                // Nếu không tìm thấy, không có gì để xóa
                throw new KeyNotFoundException($"Không tìm thấy phân bổ tài sản với mã {maTaiSan}.");
            }

            _context.TaiSans.Remove(taiSan);
            await _context.SaveChangesAsync();
        }
    }
}
