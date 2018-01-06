using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Net;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Xamarin.Forms;

namespace SearchMovies
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new SearchMovies.MainPage();
		}

		protected override void OnStart ()
		{
		    CheckConnection();
		}

	    protected override void OnSleep ()
		{
			MessagingCenter.Unsubscribe<App>(this, "ConnectionChanged");
		}

		protected override void OnResume ()
		{
			CheckConnection();
		}

	    private void CheckConnection()
	    {
	        CrossConnectivity.Current.ConnectivityChanged += delegate (object sender, ConnectivityChangedEventArgs args)
	        {
	            var isConnected = CrossConnectivity.Current.IsConnected;
	            MessagingCenter.Send<App, bool>(this, "ConnectionChanged", isConnected);
	        };
	    }
    }
}
