using System.Collections.ObjectModel;
using SearchMovies.Data.ViewModels;

namespace SearchMovies.Data.ViewModel
{
    class HomeViewModel
    {
        public static ObservableCollection<EpisodeViewModel> EpisodeViewModels { get; set; }

        public HomeViewModel()
        {
            EpisodeViewModels = new ObservableCollection<EpisodeViewModel>();
        }
    }
}
