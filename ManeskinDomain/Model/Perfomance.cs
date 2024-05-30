using System;
using System.Collections.Generic;

namespace ManeskinDomain.Model;

public partial class Perfomance
{
    public int PerfomanceId { get; set; }

    public int LocationId { get; set; }

    public DateOnly Date { get; set; }

    public string? Information { get; set; }

    public int TourId { get; set; }

    public int? FestivalId { get; set; }

    public virtual ICollection<FanProject> FanProjects { get; set; } = new List<FanProject>();

    public virtual Location Location { get; set; } = null!;
    public virtual Tour Tour { get; set; }
}
