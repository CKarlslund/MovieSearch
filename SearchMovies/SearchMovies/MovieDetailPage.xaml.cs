using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetailPage : SearchPageBase
	{
	    private readonly Movie _details;

		public MovieDetailPage (Movie details)
		{
			InitializeComponent ();

		    _details = details;

            WatchSwitch.Toggled += WatchSwitchOnToggled;
		}

	    private void WatchSwitchOnToggled(object sender, ToggledEventArgs toggledEventArgs)
	    {
            Analytics.TrackEvent("Watched switch toggled.",new Dictionary<string, string>(){{"Value", toggledEventArgs.Value.ToString()}});

	        if (toggledEventArgs.Value)
	        {
	            AddToWatched(_details.imdbID);
	        }
	        else
	        {
	            RemoveFromWatched(_details.imdbID);
	        }
	    }

	    public override async void UpdateElements()
	    {
	        Title = $"{_details.Title} ({_details.Year})";
	        PlotLabel.Text = _details.Plot;
	        ActorsLabel.Text = _details.Actors;
	        WatchSwitch.IsToggled = await HasWatched(_details.imdbID);
        }
	}
}