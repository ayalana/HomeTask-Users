using HomeTask_Users;
using HomeTask_Users.Adapters.DummyJsonAdapter;
using HomeTask_Users.Adapters.RandomuserAdapter;
using HomeTask_Users.Adapters.ReqResAdapter;

await ExportUsersToFile();

async Task ExportUsersToFile()
{
    string folderPath = GetFolderPath();
    string fileFormat = GetFileFormat();

    string filePath = Path.Combine(folderPath, $"users.{fileFormat.ToLower()}");
    IUserExporter userExporter = GetUserExporter(fileFormat);

    List<User> combinedList = await GetCombinedUserList();

    File.WriteAllText(filePath, userExporter.ExportData(combinedList));
}

string GetFolderPath()
{
    Console.WriteLine("Enter the path to the folder where the file will be stored:");
    return Console.ReadLine();
}

string GetFileFormat()
{
    Console.WriteLine("Enter the desired file format (JSON or CSV):");
    return Console.ReadLine();
}

IUserExporter GetUserExporter(string fileFormat)
{
    if (fileFormat.ToLower() == "csv")
    {
        return new CsvUserExporter();
    }
    else if (fileFormat.ToLower() == "json")
    {
        return new JsonUserExporter();
    }
    else
    {
        throw new ArgumentException("Invalid file format. Please enter 'JSON' or 'CSV'.");
    }
}

async Task<List<User>> GetCombinedUserList()
{
    var randomUser = new RandomUserDataRetriever();
    var reqRes = new ReqResDataRetriever();
    var dummyJson = new DummyJsonRetriever();

    Task<List<User>> randomUserTask = randomUser.GetUsers();
    Task<List<User>> reqResTask = reqRes.GetUsers();
    Task<List<User>> dummyJsonTask = dummyJson.GetUsers();

    await Task.WhenAll(randomUserTask, reqResTask, dummyJsonTask);

    var combinedList = new List<User>();
    combinedList.AddRange(randomUserTask.Result);
    combinedList.AddRange(reqResTask.Result);
    combinedList.AddRange(dummyJsonTask.Result);

    return combinedList;
}
