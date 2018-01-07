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
	public partial class MovieDetailPage : ContentPage
	{
		public MovieDetailPage (Movie details)
		{
			InitializeComponent ();

		    Title = $"{details.Title} ({details.Year})";
		    PlotLabel.Text = details.Plot;
		    ActorsLabel.Text = details.Actors;
		}
	}
}