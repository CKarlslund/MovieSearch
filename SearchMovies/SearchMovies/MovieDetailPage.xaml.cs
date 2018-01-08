using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieDetailPage : SearchPageBase
	{
	    private readonly string _imdbId;

		public MovieDetailPage (Movie details)
		{
			InitializeComponent ();

		    _imdbId = details.imdbID;

		    Title = $"{details.Title} ({details.Year})";
		    PlotLabel.Text = details.Plot;
		    ActorsLabel.Text = details.Actors;
		    WatchSwitch.IsToggled = HasWatched(details.imdbID);

            WatchSwitch.Toggled += WatchSwitchOnToggled;
		}

	    private void WatchSwitchOnToggled(object sender, ToggledEventArgs toggledEventArgs)
	    {
	        if (toggledEventArgs.Value)
	        {
	            AddToWatched(_imdbId);
	        }
	        else
	        {
	            RemoveFromWatched(_imdbId);
	        }
	    }

	    public override void UpdateElements()
	    {
	    }
	}
}