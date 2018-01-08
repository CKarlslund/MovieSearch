using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;
using SearchMovies.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveAndLoad))]
namespace SearchMovies.Droid
{
    class SaveAndLoad : ISaveAndLoad
    {
        private string _filePath;

        public SaveAndLoad()
        {
            var fileAccessHelper = new FileAccessHelper();
            _filePath = fileAccessHelper.GetLocalFilePath("ImdbIds");
        }

        public async Task<bool> SaveImdbId(string imdbId)
        {
            try
            {
                    using (StreamWriter streamWriter = File.AppendText(_filePath))
                    {
                        await streamWriter.WriteLineAsync(imdbId);
                    }

                return true;
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Write to file", new Dictionary<string, string>(){{"FileError", e.Message}});
            }

            return false;
        }

        public string LoadImdbIds()
        {
            try
            {
                var file =  System.IO.File.ReadAllText(_filePath);

                return file;
            }
            catch (Exception e)
            {
                Analytics.SetEnabledAsync(true);
                Analytics.TrackEvent("Read file", new Dictionary<string, string>() { { "Read file", e.Message } });
                return null;
            }

        }

        public void RemoveImdbId(string imdbId)
        {
            try
            {
                var linesToKeep = File.ReadLines(_filePath).Where(l => l != imdbId);

                File.WriteAllLines(_filePath, linesToKeep);
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Remove id", new Dictionary<string, string>() { { "Remove from file", e.Message } });
            }
        }
    }
}