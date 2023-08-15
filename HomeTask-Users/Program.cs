
using HomeTask_Users;

using JsonSerializer = System.Text.Json.JsonSerializer;

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



