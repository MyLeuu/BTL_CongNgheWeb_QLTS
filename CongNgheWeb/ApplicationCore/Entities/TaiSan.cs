using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApplicationCore.Entities;

public partial class TaiSan
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
    public int? ThoiGianBaoHanh { get; set; }


    [JsonIgnore]
    public virtual LoaiTaiSan? MaLoaiTaiSanNavigation { get; set; }
    [JsonIgnore]
    public virtual ICollection<PhanBoTaiSan> PhanBoTaiSans { get; set; } = new List<PhanBoTaiSan>();
}
