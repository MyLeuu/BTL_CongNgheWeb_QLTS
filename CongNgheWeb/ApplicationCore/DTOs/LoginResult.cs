using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DTOs
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
        public int MaNguoiDung { get; set; }
        public string Email { get; set; }
        public string HoTen { get; set; }
        public string VaiTro { get; set; }

    }
}
