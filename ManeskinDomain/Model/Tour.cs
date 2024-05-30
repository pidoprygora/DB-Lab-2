using System;
using System.Collections.Generic;

namespace ManeskinDomain.Model;

public partial class Tour
{
    public int TourId { get; set; }

    public DateOnly TourBegin { get; set; }

    public DateOnly TourEnd { get; set; }

    public string? NameTour { get; set; }
}
