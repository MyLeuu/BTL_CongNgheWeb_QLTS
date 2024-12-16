using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class PhanBoTaiSanDTO
    {
        public int MaPhanBo { get; set; }

        public int MaTaiSan { get; set; }

        public int MaPhong { get; set; }

        public int? SoLuongPhanBo { get; set; }

        public string? TinhTrang { get; set; }
    }
}
