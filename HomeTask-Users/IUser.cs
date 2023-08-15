using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTask_Users
{
    internal interface IUser
    {
        Task<User> GetUser();
    }
}
