using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Org.Apache.Http.Client.Params;
using Plugin.Connectivity;
using SearchMovies.Data;
using Xamarin.Forms;

namespace SearchMovies
{
    public abstract class SearchPageBase : ContentPage
    {
        public bool IsConnected;
        public string Url = "http://www.omdbapi.com/?apikey=3818f875&";
        public HttpClient _client;

        public SearchPageBase()
        {
            _client = new HttpClient();
        }

        protected override void OnAppearing()
        {
            CheckConnection();

            MessagingCenter.Subscribe<App, bool>(this, "ConnectionChanged", (sender, arg) =>
            {
                IsConnected = arg;
                UpdateElements();
            });
        }

        private void CheckConnection()
        {
            IsConnected = CrossConnectivity.Current.IsConnected;
            UpdateElements();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<App>(this, "ConnectionChanged");
        }

        public abstract void UpdateElements();

        public async Task<SearchResult> Search(string type, string searchText, int? resultsPage)
        {
            if (resultsPage == null)
            {
                resultsPage = 1;
            }

                _client.DefaultRequestHeaders.Add("Accept", "application/json");

                var requestString = Url + "s=" + searchText + "&type=" + type + "&page=" + resultsPage;

                var json = await _client.GetStringAsync(requestString);

                var searchResult = JsonConvert.DeserializeObject<SearchResult>(json);

                if (searchResult.Response == "True")
                {
                    return searchResult;
                }

                return new SearchResult();
        }

        public async Task<DetailedMovieSearch> GetMovieDetails(string imdbId)
        {
                var requestString = Url + "i=" + imdbId + "&plot=full";
                _client.DefaultRequestHeaders.Add("Accept", "application/json");
                var json = await _client.GetStringAsync(requestString);

                var searchResult = JsonConvert.DeserializeObject<DetailedMovieSearch>(json);

                return searchResult;
        }
    }
}