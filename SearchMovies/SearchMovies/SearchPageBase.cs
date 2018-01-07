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
        public IMovieRepository Repository;

        public SearchPageBase()
        {
            Repository = new MovieRepository();
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
    }
}