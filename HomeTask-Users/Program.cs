using System;
using System.IO;
using HomeTask_Users;
using Newtonsoft.Json;
using HomeTask_Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

static async Task TestApi(string url)
{

    
}






//HttpResponseMessage response = await client.GetAsync("https://randomuser.me/api/");

//if (response.IsSuccessStatusCode)
//{
//    string json = await response.Content.ReadAsStringAsync();
//    dynamic data = JsonConvert.DeserializeObject<dynamic>(json);

//    // Access the required data from the response
//    string firstName = data.results[0].name.first;
//    string lastName = data.results[0].name.last;
//    string email = data.results[0].email;

//    // Map the data to a User object
//    User user = new User
//    {
//        FirstName = firstName,
//        LastName = lastName,
//        Email = email
//    };

//    // Use the user object as needed
//    Console.WriteLine($"First Name: {user.FirstName}");
//    Console.WriteLine($"Last Name: {user.LastName}");
//    Console.WriteLine($"Email: {user.Email}");
//}
//else
//{
//    Console.WriteLine("Failed to retrieve data from the API.");
//}


//Console.WriteLine($"Testing API: {url}");
//HttpResponseMessage response = await client.GetAsync(url);

//if (response.IsSuccessStatusCode)
//{
//    string json = await response.Content.ReadAsStringAsync();
//    dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
//    if (url.Contains("randomuser.me"))
//    {
//        // Access the required data from the response
//        string firstName = data.results[0].name.first;
//        string lastName = data.results[0].name.last;
//        string email = data.results[0].email;

//    }
//    else if (url.Contains("jsonplaceholder.typicode.com"))
//    {
//        // Access the required data from the response
//        string firstName = data.results[0].name.first;
//        string lastName = data.results[0].name.last;
//        string email = data.results[0].email;

//    }
//    User user = new User
//    {
//        FirstName = firstName,
//        LastName = lastName,
//        Email = email
//    };

//    Console.WriteLine();
//}




//    Console.WriteLine($"Testing API: {url}");
//    List<User> users = new List<User>();    
//    HttpClient client = new HttpClient();
//    HttpResponseMessage response = await client.GetAsync(url);
//    string firstName = "";
//    string lastName = "";
//    string email = "";
//    if (response.IsSuccessStatusCode)
//    {
//        string json = await response.Content.ReadAsStringAsync();
//        dynamic data = JsonConvert.DeserializeObject<dynamic>(json);
//        if (url.Contains("randomuser.me"))
//        {
//            User user=new User();   
//            // Access the required data from the response
//            user.FirstName = data.results[0].name.first;
//            user.LastName = data.results[0].name.last;
//            user.Email = data.results[0].email;
//            users.Add(user);

//        }
//        else if (url.Contains("jsonplaceholder.typicode.com"))
//        {
//            for (int i = 0; i < data.Count; i++)
//            {
//                User user = new User();
//                string fullName = (string)data[i].name;
//                string[] names = fullName.Split(' ');
//                user.FirstName = names[0];
//                user.LastName = names[1];
//                user.Email = data[i].email;
//                users.Add(user);
//            }
//        }
//        //else if (url.Contains("dummyjson.com"))
//        //{
//        //    for (int i = 0; i < data.Count; i++)
//        //    {
//        //        User user = new User();

//        //        user.FirstName = data.users[i].;
//        //        user.LastName = names[1];
//        //        user.Email = data[i].email;
//        //        users.Add(user);
//        //    }// Access the required data from the response

//        //}
//        else
//        {
//            Console.WriteLine("Unhandled API");
//        }

//        for (int i = 0; i < users.Count; i++)
//        {
//            Console.WriteLine($"First Name: {users[i].FirstName}");
//            Console.WriteLine($"Last Name: {users[i].LastName}");
//            Console.WriteLine($"Email: {users[i].Email}");
//        }
//    }
//    else
//    {
//        Console.WriteLine($"Failed to retrieve data from the API: {url}");
//    }

//    Console.WriteLine();
//}


HttpClient client = new HttpClient();
Console.WriteLine("Enter the path to the folder where the file will be stored:");
string folderPath = Console.ReadLine();

Console.WriteLine("Enter the desired file format (JSON or CSV):");
string fileFormat = Console.ReadLine();
var dataRetriever = new DataRetriever();
var users = await dataRetriever.GetUsers();

if (users.Count > 0)
{
    string filePath = Path.Combine(folderPath, $"users.{fileFormat.ToLower()}");

    switch (fileFormat.ToLower())
    {
        case "json":
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText(filePath, json);
            break;

        case "csv":
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(users);
            }
            break;

        default:
            Console.WriteLine("Invalid file format specified.");
            break;
    }

    Console.WriteLine($"Users data saved to {filePath}.");
    Console.WriteLine($"Total number of users: {users.Count}");
}
else
{
    Console.WriteLine("No users data found.");
}



