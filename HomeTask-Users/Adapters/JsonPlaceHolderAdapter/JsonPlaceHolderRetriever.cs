using HomeTask_Users.Adapters.JsonPlaceHolderAdapter.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users.Adapters.JsonPlaceHolderAdapter
{
    internal class JsonPlaceHolderRetriever:BaseDataRetriever
    {
        public override string SourceUrl { get; set; } = "https://jsonplaceholder.typicode.com/users";

        public override async Task<List<User>> GetUsers()
        {
            var data = await GetData<JsonPlaceHolderAdapter.Entities.Users>();
            var users = data.Map();

            return users;
        }
    }
}
