using System;
using System.Collections.Generic;

namespace ManeskinDomain.Model;

public partial class Festival
{
    public int FestivalId { get; set; }

    public int PerfomanceId { get; set; }

    public string FestivalName { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int LocationId { get; set; }

    public virtual Location Location { get; set; } = null!;
}
