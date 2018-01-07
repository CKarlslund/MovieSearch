using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeriesDetailPage : ContentPage
	{
	    private IMovieRepository _repository;

		public SeriesDetailPage (Series series)
		{
            _repository = new MovieRepository();

			InitializeComponent ();

            AIndicator.IsRunning = true;
		    Title = $"{series.Title} ({series.Year})";
		    PlotLabel.Text = series.Plot;
		    ActorsLabel.Text = series.Actors;

            CreateSeasonList(series);
		    
            SeasonListView.ItemTapped += SeasonListViewOnItemTapped;
		    AIndicator.IsRunning = false;
		}

	    private async void SeasonListViewOnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var season = (SeasonSearch)e.Item;	        
	        await Navigation.PushAsync(new SeasonDetailPage(season));
        }

	    private async void CreateSeasonList(Series series)
	    {
	        var seasons = int.Parse(series.totalSeasons);

	        var seasonList = new List<SeasonSearch>();

	        for (var i = 1; i <= seasons; i++)
	        {
	            var season = await _repository.GetSeason(series.imdbID, i);
                seasonList.Add(season);
	        }

            Device.BeginInvokeOnMainThread(() =>
            {
                SeasonListView.ItemsSource = seasonList;
            });
        }


    }
}