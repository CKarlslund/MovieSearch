using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MovieSearchPage : SearchPageBase
	{
		public MovieSearchPage ()
		{
		    this.InitializeComponent();

		    SearchInput.Completed += SearchButtonOnPressed;
		    SearchButton.Pressed += SearchButtonOnPressed;
		    ResultsListView.ItemTapped += ResultSelected;
		}

	    private async void ResultSelected(object sender, ItemTappedEventArgs e)
	    {
	        var searchItem = (Search)e.Item;
	        var movieId = searchItem.imdbID;
	        var details = await Repository.GetMovieDetails(movieId);

	        await Navigation.PushAsync(new MovieDetailPage(details));
        }

	    private async void SearchButtonOnPressed(object sender, EventArgs eventArgs)
	    {
	        if (IsConnected)
	        {
	            AIndicator.IsRunning = true;
	            var searchResult = await Repository.Search("movie", SearchInput.Text, null);

	            ResultsListView.ItemsSource = searchResult.Search;
	            ResultNumber.Text = "Results: " + searchResult.totalResults;
	            AIndicator.IsRunning = false;
            }
	    }

        public override void UpdateElements()
	    {
	        if (IsConnected)
	        {
	            this.Title = "Movie Search";
	            SearchButton.IsEnabled = true;

	        }
            else if (!IsConnected)
	        {
	            this.Title = "Movie Search (no connection)";
	            SearchButton.IsEnabled = false;
	        }
	    }
	}
}