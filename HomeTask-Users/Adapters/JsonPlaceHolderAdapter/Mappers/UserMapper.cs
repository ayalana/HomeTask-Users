using System;

namespace HomeTask_Users.Adapters.JsonPlaceHolderAdapter.Mappers
{
    internal static class UserMapper
    {
        internal static List<User> Map(this JsonPlaceHolderAdapter.Entities.Users users)
        {
            return users.users.Select(u => new User
            {
                FirstName = (u.name.Split(' '))[0],
                LastName = (u.name.Split(' '))[1],
                Email = u.email,
                SourceId = 3
            }).ToList();
        }
    }
}