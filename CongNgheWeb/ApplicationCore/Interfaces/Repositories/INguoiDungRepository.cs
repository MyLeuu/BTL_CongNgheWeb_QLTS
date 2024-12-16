using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories
{ 
    public interface INguoiDungRepository
    {
        // Lấy tất cả người dùng
        Task<List<NguoiDung>> GetAllAsync();

        // Lấy người dùng theo ID
        Task<NguoiDung> GetByIdAsync(int id);

        // Thêm người dùng mới
        Task AddAsync(NguoiDung nguoiDung);

        // Cập nhật thông tin người dùng
        Task UpdateAsync(NguoiDung nguoiDung);

        // Xóa người dùng
        Task DeleteAsync(int id);
        Task<NguoiDung> GetByEmailAndPasswordAsync(string email, string matKhau);
   

    }
}
