using System;
using System.Collections.Generic;

namespace ManeskinDomain.Model;

public partial class Member
{
    public int MemberId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Age { get; set; }

    public string Instrument { get; set; } = null!;
}
