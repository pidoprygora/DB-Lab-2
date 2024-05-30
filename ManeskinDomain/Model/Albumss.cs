using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManeskinDomain.Model;

public partial class Albumss
{
    public int AlbumId { get; set; }
	[Required(ErrorMessage ="Can`t be empty")]
	[Display(Name = "Tittle")]

    public string Tittle { get; set; } = null!;
	[Required(ErrorMessage = "Can`t be empty")]
	[Display(Name = "Released")]

	public DateOnly YearRelease { get; set; }
	[Required(ErrorMessage = "Can`t be empty")]
	[Display(Name = "Songs")]

	public int Length { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
