using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users.Adapters
{
    internal abstract class BaseDataRetriever
    {

        private HttpClient httpClient = new HttpClient();
        public abstract string SourceUrl { get; set; }

        public abstract Task<List<User>> GetUsers();

        public async Task<T?> GetData<T>()
        {
            Debug.WriteLine($"Getting data from {SourceUrl}");
            HttpResponseMessage response = await httpClient.GetAsync(SourceUrl);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(json);
                Debug.WriteLine($"Successfully got data from {SourceUrl}");
                return data;
            }

            Debug.WriteLine($"Failed to get data from {SourceUrl}");
            return default;
        }


    }
}
