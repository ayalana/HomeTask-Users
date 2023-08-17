using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users.Adapters.RandomuserAdapter.Mappers
{
    internal static class UserMapper
    {
        internal static List<User> Map(this RandomuserAdapter.Entities.Users users) 
        {

            return users.results.Select(u => new User
            {
                FirstName = u.name.first,
                LastName = u.name.last,
                Email = u.email,
                SourceId = 1
            }).ToList();
        }
    }
}
