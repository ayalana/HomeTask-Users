using HomeTask_Users.Adapters.RandomuserAdapter.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users.Adapters.RandomuserAdapter
{
    internal class RandomUserDataRetriever : BaseDataRetriever
    {
        public override string SourceUrl { get; set; } = "https://randomuser.me/api/";

        public override async Task<List<User>> GetUsers()
        {
            var data = await GetData<RandomuserAdapter.Entities.Users>();
            var users = data.Map();

            return users;
        }

    }
}
