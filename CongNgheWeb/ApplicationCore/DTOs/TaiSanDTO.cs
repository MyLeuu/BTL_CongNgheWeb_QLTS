using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class TaiSanDTO
    {
        public int MaTaiSan { get; set; }
        public string ?TenTaiSan { get; set; }

        public int MaLoaiTaiSan { get; set; }

        public DateOnly? NgayMua { get; set; }

        public decimal? GiaTri { get; set; }

        public int? SoLuong { get; set; }

        public string? TinhTrang { get; set; }

        public string? HinhAnh { get; set; }

        public string? MaQr { get; set; }
    }
}
