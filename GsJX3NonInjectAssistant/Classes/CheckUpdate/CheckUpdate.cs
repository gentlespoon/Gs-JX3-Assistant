using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using GsJX3NIA.Classes.CheckUpdate;
using System.Windows.Media;

namespace GsJX3NIA
{
    public static class CheckUpdate
    {

        private const string url = "https://api.github.com/repos/gentlespoon/Gs-JX3-NonInjection-Assistant/releases";
        public const string ReleaseUrl = "https://github.com/gentlespoon/Gs-JX3-NonInjection-Assistant/releases";
        private const string urlParameters = "";

        public static List<KeyValuePair<string, string>> newerVersions = new List<KeyValuePair<string, string>>();

        public static async Task<bool> Check()
        {
            try
            {
                await GetNewerReleases();
                if (newerVersions.Count > 0)
                {
                    newerVersions = newerVersions.OrderBy(release => release.Key).ToList();
                    MainWindow.SetVersionStatus($"发现新版本：{newerVersions[newerVersions.Count-1].Key}", new SolidColorBrush(Colors.Red));
                    return true;
                }
                else
                {
                    MainWindow.SetVersionStatus("已是最新版本", new SolidColorBrush(Colors.Black));
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }

        private static async Task GetNewerReleases()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "GsNIA v" + Constants.Version);

            HttpResponseMessage response = await client.GetAsync(urlParameters);
            if (response.IsSuccessStatusCode)
            {
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                // Parse the response body.
                string responseJson = response.Content.ReadAsStringAsync().Result;
                List<GitHubApiResponseObject> releases = JsonConvert.DeserializeObject<List<GitHubApiResponseObject>>(responseJson);

                foreach (GitHubApiResponseObject release in releases)
                {
                    if (string.Compare(release.tag_name, Constants.Version) > 0)
                    {
                        newerVersions.Add(new KeyValuePair<string, string>(release.tag_name, release.body));
                    }
                }
            }
            else
            {
                throw new Exception("Failed to get release info.");
            }
            client.Dispose();
        }
    }
}
