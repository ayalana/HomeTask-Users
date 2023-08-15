using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeTask_Users
{
    internal class DataRetriever
    {
        private HttpClient _httpClient;

        public DataRetriever()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<User>> GetUsers()
        {
            var users = new List<User>();
            int count = 0;

            try
            {
                users.AddRange(await FetchUsersFromApi("https://randomuser.me/api/"));
                users.AddRange(await FetchUsersFromApi("https://jsonplaceholder.typicode.com/users"));
                users.AddRange(await FetchUsersFromApi("https://dummyjson.com/users"));
                users.AddRange(await FetchUsersFromApi("https://reqres.in/api/users"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
            }

            return users;
        }

        private async Task<List<User>> FetchUsersFromApi(string apiUrl)
        {
            var users = new List<User>();

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
                switch (apiUrl)
                {
                    case "https://randomuser.me/api/":
                        {

                            User user = new User();
                            // Access the required data from the response
                            user.FirstName = data.results[0].name.first;
                            user.LastName = data.results[0].name.last;
                            user.Email = data.results[0].email;
                            user.SourceId = 1;
                            users.Add(user);
                        }
                     ;
                        break;

                    case "https://jsonplaceholder.typicode.com/users":
                        {
                            for (int i = 0; i < data.Count; i++)
                            {
                                User user = new User();
                                string fullName = (string)data[i].name;
                                string[] names = fullName.Split(' ');
                                user.FirstName = names[0];
                                user.LastName = names[1];
                                user.Email = data[i].email;
                                user.SourceId = 2;
                                users.Add(user);
                            }
                        }
                        break;
                    case "https://dummyjson.com/users":
                        {
                            for (int i = 0; i < data.users.Count; i++)
                            {
                                User user = new User();
                               
                                user.FirstName = data.users[i]["firstName"];
                                user.LastName = data.users[i]["lastName"];
                                user.Email = data.users[i]["email"];
                                user.SourceId = 3;
                                users.Add(user);
                            }
                        }
                        break;
                    case "https://reqres.in/api/users":
                        {
                            data = data.data;
                            for (int i = 0; i < data.Count; i++)
                            {
                                User user = new User();

                                user.FirstName = data[i]["first_name"];
                                user.LastName =data[i]["last_name"];
                                user.Email = data[i]["email"];
                                user.SourceId = 4;
                                users.Add(user);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Unhandled API");
                    break;


                }

            }
            return users;

        }
    }
}
