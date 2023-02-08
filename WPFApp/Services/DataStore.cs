using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models.UI;
using Realms;
namespace WPFApp.Services
{
    public class DataStore : IDataStore
    {
        public async Task<User> Login(string username, string password)
        {
            Models.DB.User ruser = new Models.DB.User();
            User u = new User();
            try
            {
                using (Realm realm = Realm.GetInstance())
                {
                    var rdata = realm.All<Models.DB.User>();
                    ruser = realm.All<Models.DB.User>().Where(x => x.Username == username).FirstOrDefault();
                    u.ID = ruser.ID;
                    u.ID = ruser.ID;

                    u.Password = ruser.Password;
                    u.Fname = ruser.Fname;
                    u.Lname = ruser.Lname;
                    u.Role = ruser.Role;
                }


            }
            catch (Exception e)
            {
                return null;
            }
            if (password == u.Password && !String.IsNullOrEmpty(password))
            {
                return u;
            }
            else
            {
                return null;
            }

        }

        public async Task<User> Registration(User user)
        {
            User u = new User();
            u = user;
            bool userisalreadyexists = UserAlreadyExists(u.Username);
            if (!userisalreadyexists)
            {
                try
                {
                    using (Realm realm = Realm.GetInstance())
                    {
                        realm.Write(() =>
                        {
                            realm.Add(new Models.DB.User(u), true);
                        });
                    }
                    return user;
                }

                catch (Exception e)
                {
                    throw new Exception();
                }
            }
            else
            {
                return null;
            }
          
        }
        public  bool UserAlreadyExists(string username)
        {
            Models.DB.User dbu = new Models.DB.User();
           List<Models.DB.User> ulist= new List<Models.DB.User>();
            try
            {
                using (Realm realm = Realm.GetInstance())
                {
                    ulist = realm.All<Models.DB.User>().Where(x => x.Username == username).ToList();
                    return ulist.Count > 0;
                }
            }
            catch (Exception e)
            {
                return true;
            }
        }
    }
}
