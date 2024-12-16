using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApplicationCore.Entities;

public partial class BaoTri
{
    public int? MaBaoTri { get; set; }

    public int? MaPhanBo { get; set; }

    public DateOnly? NgayBaoTri { get; set; } = null;

    public string? TrangThai { get; set; }

    public string? NguoiThucHien { get; set; }

    public DateOnly? NgayHoanThanh { get; set; } = null;

    public int? SoLuongBaoTri { get; set; }

    [JsonIgnore]
    public virtual PhanBoTaiSan? MaPhanBoNavigation { get; set; } = null!;
}
