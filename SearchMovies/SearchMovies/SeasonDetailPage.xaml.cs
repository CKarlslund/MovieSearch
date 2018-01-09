using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AppCenter.Analytics;
using SearchMovies.Data;
using SearchMovies.Data.ViewModel;
using SearchMovies.Data.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeasonDetailPage : SearchPageBase
	{
	    private readonly SeasonSearch _season;

	    public SeasonDetailPage(SeasonSearch season)
	    {
	        _season = season;

	        InitializeComponent();

	        PopulateListView(season.Episodes);
            EpisodeListView.ItemTapped += EpisodeListViewOnItemTapped;
	    }

	    private void EpisodeListViewOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Analytics.TrackEvent("EpisodeList tapped");
            var episodeViewModel = (EpisodeViewModel) e.Item;
            episodeViewModel.PlotIsVisible = !episodeViewModel.PlotIsVisible;

            var indexOfEpisode = HomeViewModel.EpisodeViewModels.IndexOf(episodeViewModel);
            HomeViewModel.EpisodeViewModels.Remove(episodeViewModel);
            HomeViewModel.EpisodeViewModels.Insert(indexOfEpisode, episodeViewModel);
        }

        private async void PopulateListView(EpisodeSearch[] episodes)
	    {
	        AIndicator.IsRunning = true;
	        EpisodeListView.ItemsSource = new ObservableCollection<EpisodeViewModel>();

	        var models = new ObservableCollection<EpisodeViewModel>();

            foreach (var episode in episodes)
	        {
	            var detailedEpisode = await Repository.GetEpisode(episode.imdbID);

	            var episodeViewModel = new EpisodeViewModel
	            {
	                Number = detailedEpisode.Episode,
	                Plot = detailedEpisode.Plot,
	                Title = detailedEpisode.Title,
                    ImdbId = detailedEpisode.imdbID,
                    HasWatched = await HasWatched(detailedEpisode.imdbID),
                    BackgroundColorProperty = await HasWatched(detailedEpisode.imdbID) ? "darkSeaGreen" : "white",
	                PlotIsVisible = false
	            };
	            models.Add(episodeViewModel);
	        }

	        HomeViewModel.EpisodeViewModels = models;

	        EpisodeListView.ItemsSource = HomeViewModel.EpisodeViewModels;
	        AIndicator.IsRunning = false;
	    }

	    public override void UpdateElements()
	    {
	        if (IsConnected)
	        {
	            Title = $"{_season.Title} Season {_season.Season}";
            }
	        else if (!IsConnected)
	        {
	            Title = $"{_season.Title} - Not Connected";
            }
	    }

	    private async void Switch_OnToggled(object sender, ToggledEventArgs e)
	    {
            Analytics.TrackEvent("HasWatchedSwitch toggled", new Dictionary<string, string>(){{"Value", e.Value.ToString()}});
	        ListView listView = (ListView)((Switch) sender).Parent.Parent.Parent.Parent;

	        var episode = (EpisodeViewModel)listView?.SelectedItem;

	        var hasWatchedInFile = episode != null && await HasWatched(episode.ImdbId);

	        if (episode != null && hasWatchedInFile != episode.HasWatched)
	        {
	            if (!episode.HasWatched && hasWatchedInFile)
	            {
	                RemoveFromWatched(episode.ImdbId);
	            }
	            else
	            {
	                AddToWatched(episode.ImdbId);
	            }
	        }

            UpdateElements();
	    }
	}
}