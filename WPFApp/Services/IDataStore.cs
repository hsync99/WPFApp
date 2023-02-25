using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WPFApp.Models.UI;
namespace WPFApp.Services
{
    public interface IDataStore
    {
        public Task<User> Login(string username,string password);
        public Task<User> Registration(User user);
        public Task<User> GetUserByid(string userid);
    }
}
