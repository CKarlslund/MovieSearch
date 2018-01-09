using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeriesDetailPage : SearchPageBase
	{
	    private readonly Series _series;

		public SeriesDetailPage (Series series)
		{
		    _series = series;

			InitializeComponent ();

		    PlotLabel.Text = series.Plot;
		    ActorsLabel.Text = series.Actors;

		    SeasonListView.ItemTapped += SeasonListViewOnItemTapped;
		    LinkButton.Clicked += LinkButtonOnClicked;

            CreateSeasonList();		   
		}

	    private void LinkButtonOnClicked(object sender, EventArgs eventArgs)
	    {
            Analytics.TrackEvent("External Link Button Clicked");
            Device.OpenUri(new Uri("http://www.imdb.com/title/" + _series.imdbID));
	    }

	    private async void SeasonListViewOnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        if (IsConnected)
	        {
	            var season = (SeasonSearch)e.Item;

	            Analytics.TrackEvent("Season Item Tapped", new Dictionary<string, string>() { { "Season", season.Title + " Season " + season.Season } });

	            await Navigation.PushAsync(new SeasonDetailPage(season));
            }
        }

	    private async void CreateSeasonList()
	    {
	        AIndicator.IsRunning = true;

            var seasons = int.Parse(_series.totalSeasons);

	        var seasonList = new List<SeasonSearch>();

	        for (var i = 1; i <= seasons; i++)
	        {
	            var season = await Repository.GetSeason(_series.imdbID, i);
                seasonList.Add(season);
	        }

            Device.BeginInvokeOnMainThread(() =>
            {
                SeasonListView.ItemsSource = seasonList;
            });
	        AIndicator.IsRunning = false;
	    }


	    public override void UpdateElements()
	    {
	        if (IsConnected)
	        {
	            Title = $"{_series.Title} ({_series.Year})";
	        }
            else if (!IsConnected)
	        {
	            Title = $"{_series.Title} - Not Connected";
	        }

        }
	}
}