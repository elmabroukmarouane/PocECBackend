using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Ecare.Infrastructure.Models.Seeds
{
    public class UserSeed
    {
        public static IList<User> GetUsersMockUp()
        {
            return new List<User>()
        {
            new User()
            {
                UserId = 1,
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                Email = "user1@mail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                IsConnected = 0
            },
            new User()
            {
                UserId = 2,
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                Email = "user2@mail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                IsConnected = 0
            },
            new User()
            {
                UserId = 3,
                FirstName = "FirstName 3",
                LastName = "LastName 3",
                Email = "user3@mail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                IsConnected = 0
            },
            new User()
            {
                UserId = 4,
                FirstName = "FirstName 4",
                LastName = "LastName 4",
                Email = "user4@mail.com",
                Password = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413",
                IsConnected = 0
            }
        };
        }
    }
}
