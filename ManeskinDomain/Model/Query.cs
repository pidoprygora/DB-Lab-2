using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManeskinDomain.Model
{
    public class Query
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public int Duration { get; set; }
        public int AlbumId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int ToursVisited { get; set; }
        public int SongCount { get; set; }
        public int LocationId { get; set; }
        public int ConcertCount { get; set; }
    }
}
