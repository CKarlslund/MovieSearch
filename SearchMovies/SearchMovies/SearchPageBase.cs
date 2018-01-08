using Plugin.Connectivity;
using SearchMovies.Data;
using SearchMovies.Droid;
using Xamarin.Forms;

namespace SearchMovies
{
    public abstract class SearchPageBase : ContentPage
    {
        public bool IsConnected;
        public IMovieRepository Repository;

        protected SearchPageBase()
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

        public bool HasWatched(string imdbId)
        {
            var watchedIds = DependencyService.Get<ISaveAndLoad>().LoadImdbIds();

            if (watchedIds != null && watchedIds.Contains(imdbId))
            {
                return true;
            }

            return false;
        }

        public void AddToWatched(string imdbId)
        {
            if (!HasWatched(imdbId))
            {
                DependencyService.Get<ISaveAndLoad>().SaveImdbId(imdbId);
            }
        }

        public void RemoveFromWatched(string imdbId)
        {
            DependencyService.Get<ISaveAndLoad>().RemoveImdbId(imdbId);
        }
    }
}