using System;
using System.Collections.Generic;

namespace ManeskinDomain.Model;

public partial class Clip
{
    public int VideoId { get; set; }

    public int SongId { get; set; }

    public DateOnly DataRelease { get; set; }

    public string? MadeBy { get; set; }

    public virtual Song Song { get; set; } = null!;
}
