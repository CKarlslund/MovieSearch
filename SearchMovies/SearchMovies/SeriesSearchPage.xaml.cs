using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using SearchMovies.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeriesSearchPage : SearchPageBase
	{
	    public SeriesSearchPage()
	    {
	        this.InitializeComponent();
            Analytics.TrackEvent("Entered Series Search");

	        SearchInput.Completed += SearchButtonOnPressed;
	        SearchButton.Pressed += SearchButtonOnPressed;
	        ResultsListView.ItemTapped += ResultSelected;
	    }

	    private async void ResultSelected(object sender, ItemTappedEventArgs e)
	    {
	        var searchItem = (Search)e.Item;
	        var seriesId = searchItem.imdbID;

            Analytics.TrackEvent("Result selected.", new Dictionary<string, string>(){{"ImdbId", seriesId}});

	        var details = await Repository.GetSeriesDetails(seriesId);

	        await Navigation.PushAsync(new SeriesDetailPage(details));
	    }

	    private async void SearchButtonOnPressed(object sender, EventArgs eventArgs)
	    {
            if (IsConnected)
            { 
	            AIndicator.IsRunning = true;
	            var searchResult = await Repository.Search("series", SearchInput.Text, null);

                Analytics.TrackEvent("Search button pressed", new Dictionary<string, string>() { {"Search word", SearchInput.Text},{"Response", searchResult.Response } });

                ResultsListView.ItemsSource = searchResult.Search;
	            ResultNumber.Text = "Results: " + searchResult.totalResults;
	            AIndicator.IsRunning = false;
            }
        }

	    public override void UpdateElements()
	    {
	        if (IsConnected)
	        {
	            this.Title = "Series Search";
	            SearchButton.IsEnabled = true;

	        }
	        else if (!IsConnected)
	        {
	            this.Title = "Series Search (no connection)";
	            SearchButton.IsEnabled = false;
	        }
	    }
    }
}