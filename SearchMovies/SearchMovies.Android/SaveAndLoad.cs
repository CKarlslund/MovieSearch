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
                    using (var sw = File.AppendText(_filePath))
                    {
                        await sw.WriteLineAsync(imdbId);
                    }

                return true;
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Write to file", new Dictionary<string, string>(){{"FileError", e.Message}});
            }

            return false;
        }

        public async Task<string> LoadImdbIds()
        {
            try
            {
                string file;

                if (File.Exists(_filePath))
                {
                    using (var sr = new StreamReader(_filePath))
                    {
                        file = await sr.ReadToEndAsync();
                    }

                    return file;
                }
                else
                {
                    Analytics.TrackEvent("LoadImdbs", new Dictionary<string, string>(){{"Error", "File not found"}});
                }
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Read file", new Dictionary<string, string>() { { "Read file error", e.Message } });
                return null;
            }

            return null;
        }

        public async void RemoveImdbId(string imdbId)
        {
            try
            {
                //string line_to_delete = imdbId;

                //string[] array;

                ////remove line
                //using (var sr = new StreamReader(_filePath))
                //{
                //    string line;
                //    while ((line = sr.ReadLineAsync().Result) != null)
                //    {
                //        if (String.CompareOrdinal(line, line_to_delete) == 0)
                //            continue;

                //        await writer.WriteLineAsync(line);
                //    }
                //}              

                string newFile = "";
                using (StreamReader sr = File.OpenText(_filePath))
                {
                    string strOldText;
                    while ((strOldText = await sr.ReadLineAsync()) != null)
                    {
                        if (!strOldText.Contains(imdbId))
                        {
                            newFile += strOldText + Environment.NewLine;
                        }
                    }
                }

                using (StreamWriter sw = new StreamWriter(_filePath, false))
                {
                    //File.WriteAllText(_filePath, newFile);
                    await sw.WriteAsync(newFile);
                }
            }
            catch (Exception e)
            {
                Analytics.TrackEvent("Remove id", new Dictionary<string, string>() { { "Remove from file", e.Message } });
            }
        }
    }
}