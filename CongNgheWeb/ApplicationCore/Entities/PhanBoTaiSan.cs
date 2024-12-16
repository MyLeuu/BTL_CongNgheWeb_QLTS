using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApplicationCore.Entities;

public partial class PhanBoTaiSan
{
    public int MaPhanBo { get; set; }

    public int MaTaiSan { get; set; }

    public int MaPhong { get; set; }

    public int? SoLuongPhanBo { get; set; }

    public int? SoLuongDangBaoTri { get; set; }

    public string? TinhTrang { get; set; }

    [JsonIgnore]
    public virtual Phong? MaPhongNavigation { get; set; } 
    [JsonIgnore]
    public virtual TaiSan? MaTaiSanNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<BaoTri> BaoTris { get; set; } = new List<BaoTri>();

}
