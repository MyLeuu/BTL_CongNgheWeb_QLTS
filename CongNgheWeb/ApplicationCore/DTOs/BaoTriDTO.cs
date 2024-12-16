using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class BaoTriDTO
    {
        public int MaBaoTri { get; set; }

        public int MaTaiSan { get; set; }

        public DateOnly? NgayBaoTri { get; set; }

        public string? TrangThai { get; set; }

        public string? NguoiThucHien { get; set; }
    }
}
