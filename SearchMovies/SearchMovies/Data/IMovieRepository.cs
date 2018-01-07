using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SearchMovies.Data;

namespace SearchMovies.Data
{
    public interface IMovieRepository
    {
        Task<SeasonSearch> GetSeason(string imdbId, int season);
        Task<SeriesEpisode> GetEpisode(string imdbId);
        Task<SearchResult> Search (string type, string searchText, int? resultsPage);
        Task<Movie> GetMovieDetails(string movieId);
        Task<Series> GetSeriesDetails(string seriesId);
    }
}
