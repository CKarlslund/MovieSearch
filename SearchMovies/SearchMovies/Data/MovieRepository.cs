using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;

namespace SearchMovies.Data
{
    class MovieRepository : IMovieRepository
    {
        private string _url;
        private HttpClient _client;

        public MovieRepository()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _url = "http://www.omdbapi.com/?apikey=3818f875&";
        }

        public async Task<SeasonSearch> GetSeason(string imdbId, int season)
        {
            var requestString = _url + "i=" + imdbId + "&season=" + season;

            try
            {
                var json = await _client.GetStringAsync(requestString);

                var searchResult = JsonConvert.DeserializeObject<SeasonSearch>(json);

                return searchResult;
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("GetSeason", new Dictionary<string, string>() { { "ResponseError", e.Message } });

                return new SeasonSearch(){Response = "false"}; //TODO Check if true
            }
        }

        public async Task<SeriesEpisode> GetEpisode(string imdbId)
        {
            var requestString = _url + "i=" + imdbId + "&plot=full";

            try
            {
                var json = await _client.GetStringAsync(requestString);
                var searchResult = JsonConvert.DeserializeObject<SeriesEpisode>(json);

                return searchResult;
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("GetEpisode", new Dictionary<string, string>() { { "ResponseError", e.Message } });
                return new SeriesEpisode(){Response = "false"};
            }
        }

        public async Task<SearchResult> Search(string type, string searchText, int? resultsPage)
        {
            if (resultsPage == null)
            {
                resultsPage = 1;
            }

            searchText = searchText.Trim();

            var requestString = _url + "s=" + searchText + "&type=" + type + "&page=" + resultsPage;

            try
            {
                var json = await _client.GetStringAsync(requestString);

                var searchResult = JsonConvert.DeserializeObject<SearchResult>(json);

                if (searchResult.Response == "True")
                {
                    return searchResult;
                }
                else
                {
                    return new SearchResult { totalResults = "0", Response = "false" };
                }
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Search", new Dictionary<string, string>() { { "ResponseError", e.Message } });
                return new SearchResult { totalResults = "0", Response = "false" };
            }
        }

        public async Task<Movie> GetMovieDetails(string imdbId)
        {
            var requestString = _url + "i=" + imdbId + "&plot=full";

            try
            {
                var json = await _client.GetStringAsync(requestString);
                var searchResult = JsonConvert.DeserializeObject<Movie>(json);

                return searchResult;
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("GetMovieDetails", new Dictionary<string, string>() { { "ResponseError", e.Message } });
                return new Movie(){Response = "false"};
            }            
        }

        public async Task<Series> GetSeriesDetails(string imdbId)
        {
            var requestString = _url + "i=" + imdbId + "&plot=full";

            try
            {
                var json = await _client.GetStringAsync(requestString);
                var searchResult = JsonConvert.DeserializeObject<Series>(json);
                return searchResult;
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Search", new Dictionary<string, string>() { { "ResponseError", e.Message } });
                return new Series(){Response = "GetSeriesDetails"};
            }
        }
    }
}
