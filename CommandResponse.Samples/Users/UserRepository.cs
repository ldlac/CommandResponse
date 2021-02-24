using System.Collections.Generic;
using System.Linq;

namespace CommandResponse.Samples.Users
{
    public class UserRepository
    {
        private List<User> Users { get; set; }

        public UserRepository()
        {
            Users = new List<User>()
            {
                new User("User1")
            };
        }

        public User GetUser(string username)
        {
            return Users.FirstOrDefault(x => x.Username == username);
        }
    }
}