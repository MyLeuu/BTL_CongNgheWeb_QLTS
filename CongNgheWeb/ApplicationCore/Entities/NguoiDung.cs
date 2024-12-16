using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities;

public partial class NguoiDung
{
    public int MaNguoiDung { get; set; }

    public string HoTen { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string Email { get; set; } = null!;

    public string VaiTro { get; set; } = null!;

    public string MatKhau { get; set; } = null!;
}
