namespace SearchMovies
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
        }

        public string Title { get; set; }
        public string Year { get; set; }
        public bool HasWatched { get; set; }
        public string BackgroundColorProperty { get; set; }
        public string ImdbId { get; set; }
    }
}