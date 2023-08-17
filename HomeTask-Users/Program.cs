using HomeTask_Users;
using HomeTask_Users.Adapters;
using HomeTask_Users.Adapters.DummyJsonAdapter;
using HomeTask_Users.Adapters.RandomuserAdapter;
using HomeTask_Users.Adapters.ReqResAdapter;

List<BaseDataRetriever> adapters = new List<BaseDataRetriever>
{
    new RandomUserDataRetriever(),
    new ReqResDataRetriever(),
    new DummyJsonRetriever()
};

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
    var combinedList = new List<User>();
    var tasks = adapters.Select(async a => combinedList.AddRange(await a.GetUsers())).ToList();

    await Task.WhenAll(tasks);

    return combinedList;
}
