using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace SeaBattleWPF.API
{
    public class Client
    {
        HttpClient httpClient = new HttpClient();
        private Client()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:7138/api/");
        }

        static Client instance = new();
        public static Client Instance { get => instance; }

        internal async Task<(HttpStatusCode, T)> PostMessageAsync<T>(string path, string content = null) where T : class
        {
            HttpResponseMessage response;
            if (content != null)
                response = await httpClient.PostAsync(path, new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json"));
            else
                response = await httpClient.PostAsync(path, new StringContent(""));
            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                return (response.StatusCode, JsonSerializer.Deserialize<T>(str));
            }
            else
            {
                //MessageBox.Show($"error code {response.StatusCode} msg{await response.Content.ReadAsStringAsync()}");
                return (response.StatusCode, null);
            }
        }

        internal async Task<(HttpStatusCode, string)> PostMessagePlainAsync(string path, object content = null)
        {
            HttpResponseMessage response;
            if (content != null)
                response = await httpClient.PostAsync(path, new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json"));
            else
                response = await httpClient.PostAsync(path, new StringContent(""));
            if (response.IsSuccessStatusCode)
            {
                return (response.StatusCode, await response.Content.ReadAsStringAsync());
            }
            else
            {
                //MessageBox.Show($"error code {response.StatusCode} msg{await response.Content.ReadAsStringAsync()}");
                return (response.StatusCode, null);
            }
        }

        internal void SetToken(string token)
        {
            httpClient.DefaultRequestHeaders.
                Add("Authorization", "Bearer " + token);
        }
    }

}
