using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchMovies.Data
{
    interface IMovieRepository
    {
        Task<SeasonSearch> GetSeason(string imdbId, int season);
        Task<SeriesEpisode> GetEpisode(string imdbId);
    }
}
