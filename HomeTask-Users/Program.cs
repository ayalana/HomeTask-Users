using HomeTask_Users;
using HomeTask_Users.Adapters.DummyJsonAdapter;
using HomeTask_Users.Adapters.RandomuserAdapter;
using HomeTask_Users.Adapters.ReqResAdapter;


Console.WriteLine("Enter the path to the folder where the file will be stored:");
string folderPath = Console.ReadLine();
Console.WriteLine("Enter the desired file format (JSON or CSV):");
string fileFormat = Console.ReadLine();


string filePath = Path.Combine(folderPath, $"users.{fileFormat.ToLower()}");
IUserExporter userExporter=null;
if (fileFormat.ToLower() == "csv")
{
    userExporter = new CsvUserExporter();
}
else if (fileFormat.ToLower() == "json")
{
    userExporter = new JsonUserExporter();
}



var randomUser = new RandomUserDataRetriever();
var reqRes = new ReqResDataRetriever();
var dummyJson = new DummyJsonRetriever();

Task<List<User>> randomUserTask = randomUser.GetUsers();
Task<List<User>> reqResTask = reqRes.GetUsers();
Task<List<User>> dummyJsonTask = dummyJson.GetUsers();

await Task.WhenAll(randomUserTask, reqResTask,dummyJsonTask);

var combinedList = new List<User>();
combinedList.AddRange(randomUserTask.Result);
combinedList.AddRange(reqResTask.Result);
combinedList.AddRange(dummyJsonTask.Result);

File.WriteAllText(filePath, userExporter.ExportData(combinedList));
