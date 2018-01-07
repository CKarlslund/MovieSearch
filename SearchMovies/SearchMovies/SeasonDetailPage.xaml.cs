using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeasonDetailPage : ContentPage
	{
	    private IMovieRepository _repository;

	    public SeasonDetailPage(SeasonSearch season)
	    {
	        InitializeComponent();

	        AIndicator.IsRunning = true;
	        _repository = new MovieRepository();
	        Title = $"{season.Title} Season {season.Season}";

	        GetEpisodes(season.Episodes);
	        AIndicator.IsRunning = false;
	    }

	    private async void GetEpisodes(EpisodeSearch[] episodes)
	    {
	        var detailedEpisodes = new List<SeriesEpisode>();

	        foreach (var episode in episodes)
	        {
	            var detailedEpisode = await _repository.GetEpisode(episode.imdbID);
	            detailedEpisodes.Add(detailedEpisode);
	        }

	        EpisodeListView.ItemsSource = detailedEpisodes;
	    }
	}
}