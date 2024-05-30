using System;
using System.Collections.Generic;

namespace ManeskinDomain.Model;

public partial class Song
{
    public int SongId { get; set; }

    public string Tittle { get; set; } = null!;

    public int AlbumId { get; set; }

    public int Duration { get; set; }

    public DateOnly DataRelease { get; set; }

    public virtual Albumss Album { get; set; } = null!;

    public virtual ICollection<Clip> Clips { get; set; } = new List<Clip>();
}
