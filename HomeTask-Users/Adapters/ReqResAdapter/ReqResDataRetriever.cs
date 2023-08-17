using HomeTask_Users.Adapters.ReqResAdapter.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users.Adapters.ReqResAdapter
{
    internal class ReqResDataRetriever : BaseDataRetriever
    {
        public override string SourceUrl { get; set; } = "https://reqres.in/api/users";

        public override async Task<List<User>> GetUsers()
        {
            var data = await GetData<ReqResAdapter.Entities.Users>();
            var users = data.Map();

            return users;
        }
    }
}
