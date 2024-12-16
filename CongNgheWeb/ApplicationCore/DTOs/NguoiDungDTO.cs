using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class NguoiDungDTO
    {
        public int MaNguoiDung { get; set; }

        public string HoTen { get; set; } 

        public string? SoDienThoai { get; set; }

        public string Email { get; set; } 

        public string VaiTro { get; set; } 

        public string MatKhau { get; set; } 
    }
}
