using System.Threading.Tasks;

namespace SearchMovies.Droid
{
    internal interface ISaveAndLoad
    {
        Task<bool> SaveImdbId(string text);
        string LoadImdbIds();
        void RemoveImdbId(string imdbId);
    }
}