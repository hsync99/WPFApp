using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
namespace WPFApp.Models.DB
{
    public class User : RealmObject
    {
        [PrimaryKey]
        public string ID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public string Username { get; set; }    
        public string Password { get; set; }    
        public string Email { get; set; }
        public int Role { get; set; } = 0;
        public double Balance { get; set; }
        public User()
        {


        }
        public User(Models.UI.User u)
        {
            ID = u.ID;
            Fname = u.Fname;
            Lname = u.Lname;
            Username = u.Username;
            Password = u.Password;
            Email = u.Email;
            Role = u.Role;
            Balance = u.Balance;

        }
    }
}
