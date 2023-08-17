using HomeTask_Users.Adapters.DummyJsonAdapter.Mappers;
using System;


namespace HomeTask_Users.Adapters.DummyJsonAdapter
{
    internal class DummyJsonRetriever: BaseDataRetriever
    {
        public override string SourceUrl { get; set; } = "https://dummyjson.com/users";

        public override async Task<List<User>> GetUsers()
        {
            var data = await GetData<DummyJsonAdapter.Entities.Users>();
            var users = data.Map();

            return users;
        }
    }
}
