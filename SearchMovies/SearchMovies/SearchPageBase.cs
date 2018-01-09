using System.Threading.Tasks;
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

            UpdateElements();
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

        public async Task<bool> HasWatched(string imdbId)
        {
            var watchedIds = await DependencyService.Get<ISaveAndLoad>().LoadImdbIds();

            if (watchedIds != null && watchedIds.Contains(imdbId))
            {
                return true;
            }

            return false;
        }

        public async void AddToWatched(string imdbId)
        {
            var hasWatched = await HasWatched(imdbId);
            if (!hasWatched)
            {
                await DependencyService.Get<ISaveAndLoad>().SaveImdbId(imdbId);
            }
        }

        public void RemoveFromWatched(string imdbId)
        {
            DependencyService.Get<ISaveAndLoad>().RemoveImdbId(imdbId);
        }
    }
}