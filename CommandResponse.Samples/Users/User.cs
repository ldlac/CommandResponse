using System;
using System.Collections.Generic;
using System.Text;

namespace CommandResponse.Samples.Users
{
    public class User
    {
        public User(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}