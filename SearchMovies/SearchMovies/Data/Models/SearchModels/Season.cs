using System;
using System.Collections.Generic;
using System.Text;

namespace SearchMovies.Data
{
    public class SeasonSearch
    {
        public string Title { get; set; }
        public string Season { get; set; }
        public string totalSeasons { get; set; }
        public EpisodeSearch[] Episodes { get; set; }
        public string Response { get; set; }
    }

    public class EpisodeSearch
    {
        public string Title { get; set; }
        public string Released { get; set; }
        public string Episode { get; set; }
        public string imdbRating { get; set; }
        public string imdbID { get; set; }
    }

}
