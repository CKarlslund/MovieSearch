using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SearchMovies
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SeriesSearchPage : SearchPageBase
	{
		public SeriesSearchPage ()
		{
            InitializeComponent();
		}

	    public override void UpdateElements()
	    {

	    }
	}
}