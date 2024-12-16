using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApplicationCore.Entities;

public partial class LoaiTaiSan
{
    public int MaLoaiTaiSan { get; set; }

    public string TenLoaiTaiSan { get; set; } = null!;

    public string? MoTa { get; set; }

    [JsonIgnore]
    public virtual ICollection<TaiSan> TaiSans { get; set; } = new List<TaiSan>();
}
