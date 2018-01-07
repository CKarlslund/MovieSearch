using System.Collections.Generic;
using SearchMovies.Data;
using SearchMovies.Data.ViewModels;
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

	        AIndicator.IsRunning = true;
	        PopulateListView(season.Episodes);
	        AIndicator.IsRunning = false;
	    }

	    private async void PopulateListView(EpisodeSearch[] episodes)
	    {
	        var episodeViewModels = new List<EpisodeViewModel>();

	        foreach (var episode in episodes)
	        {
	            var detailedEpisode = await Repository.GetEpisode(episode.imdbID);
	            var episodeViewModel = new EpisodeViewModel
	            {
	                Number = detailedEpisode.Episode,
	                Plot = detailedEpisode.Plot,
	                Title = detailedEpisode.Title,
	                PlotIsVisible = false
	            };
	            episodeViewModels.Add(episodeViewModel);
	        }

	        EpisodeListView.ItemsSource = episodeViewModels;
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
	}
}