using System;

namespace HomeTask_Users.Adapters.ReqResAdapter.Mappers
{
    internal static class UserMapper
    {
        internal static List<User> Map(this ReqResAdapter.Entities.Users users)
        {
            return users.data.Select(u => new User
            {
                FirstName = u.first_name,
                LastName = u.last_name,
                Email = u.email,
                SourceId = 2
            }).ToList();
        }
    }
}
