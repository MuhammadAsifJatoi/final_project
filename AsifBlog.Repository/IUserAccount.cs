using AsifBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsifBlog.Repository
{
    public interface IUserAccount
    {
        public User GetUserForLogin(string email ,string password);
        string Register( User user);
        User GetUserInfo(string accessToken);
    }
}
