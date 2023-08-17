
namespace HomeTask_Users.Adapters.DummyJsonAdapter.Mappers
{
    internal static class UserMapper
    {
        internal static List<User> Map(this DummyJsonAdapter.Entities.Users users)
        {
            return users.users.Select(u => new User
            {
                FirstName = u.firstName,
                LastName = u.lastName,
                Email = u.email,
                SourceId = 4
            }).ToList();
        }
    }
}
