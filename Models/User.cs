using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saucedemo.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public static User StandardUser => new User
        {
            Username = "standard_user",
            Password = "secret_sauce"
        };
    }
}
