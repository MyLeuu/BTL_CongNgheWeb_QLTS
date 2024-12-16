using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PhanBoTaiSanRepository:IPhanBoTaiSanRepository
    {
        private readonly QltsContext _context;

        public PhanBoTaiSanRepository(QltsContext context)
        {
            _context = context;
        }
        public async Task<List<PhanBoTaiSan>> GetAllAsync()
        {
            return await _context.PhanBoTaiSans
                .Select(t => new PhanBoTaiSan
                {
                    MaPhanBo = t.MaPhanBo,
                    MaTaiSan = t.MaTaiSan,
                    MaPhong = t.MaPhong,
                    SoLuongPhanBo = t.SoLuongPhanBo,
                    SoLuongDangBaoTri = t.SoLuongDangBaoTri,
                    TinhTrang =t.TinhTrang,
                }).ToListAsync();
        }
        public async Task<PhanBoTaiSan> GetByIdAsync(int id)
        {
            var phanBoTaiSan = await _context.PhanBoTaiSans.FindAsync(id);

            if (phanBoTaiSan == null)
            {
                return null;
            }

            // Chuyển đổi entity NguoiDung sang NguoiDungDTO
           return phanBoTaiSan;
        }
        public async Task AddAsync(PhanBoTaiSan phanBoTaiSanDTO)
        {
            var phanBoTaiSan = new PhanBoTaiSan
            {
                MaPhanBo = phanBoTaiSanDTO.MaPhanBo,
                MaTaiSan = phanBoTaiSanDTO.MaTaiSan,
                MaPhong = phanBoTaiSanDTO.MaPhong,
                SoLuongPhanBo = phanBoTaiSanDTO.SoLuongPhanBo,
                SoLuongDangBaoTri = phanBoTaiSanDTO.SoLuongDangBaoTri,
                TinhTrang = phanBoTaiSanDTO.TinhTrang,
            };

            await _context.PhanBoTaiSans.AddAsync(phanBoTaiSanDTO);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(PhanBoTaiSan phanBoTaiSanDTO)
        {
            var existingPhanBo = await _context.PhanBoTaiSans.FindAsync(phanBoTaiSanDTO.MaPhanBo);

            if (existingPhanBo == null)
            {
                // Handle the case where the user does not exist (could throw an exception or return a specific result)
                return;
            }

            // Cập nhật thông tin từ DTO sang entity
            existingPhanBo.MaPhanBo = phanBoTaiSanDTO.MaPhanBo;
            existingPhanBo.MaTaiSan = phanBoTaiSanDTO.MaTaiSan;
            existingPhanBo.MaPhong = phanBoTaiSanDTO.MaPhong;
            existingPhanBo.SoLuongPhanBo = phanBoTaiSanDTO.SoLuongPhanBo;
            existingPhanBo.SoLuongDangBaoTri = phanBoTaiSanDTO.SoLuongDangBaoTri;
            existingPhanBo.TinhTrang = phanBoTaiSanDTO.TinhTrang;

            _context.PhanBoTaiSans.Update(existingPhanBo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int maPhanBo)
        {
            var phanBoTaiSan = await _context.PhanBoTaiSans.FindAsync(maPhanBo);

            if (phanBoTaiSan == null)
            {
                // Nếu không tìm thấy, không có gì để xóa
                throw new KeyNotFoundException($"Không tìm thấy phân bổ tài sản với mã {maPhanBo}.");
            }

            _context.PhanBoTaiSans.Remove(phanBoTaiSan);
            await _context.SaveChangesAsync();
        }

    }
}
