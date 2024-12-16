using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApplicationCore.Entities;

public partial class Phong
{
    public int MaPhong { get; set; }

    public string TenPhong { get; set; } = null!;

    public string? ViTri { get; set; }

    [JsonIgnore]
    public virtual ICollection<PhanBoTaiSan> PhanBoTaiSans { get; set; } = new List<PhanBoTaiSan>();
}
