using AsifBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsifBlog.Repository
{
    public interface IUser
    {
        //-------userRole
        public List<UserRole> GetUserRoles();
        UserRole GetRole(int id);
        public void AddUpdateUserRole(UserRole userRole);
        public void DeleteRole(int id);
        //---------user
        List<User>GetUsers();
        public User GetUser(int id);
        public void AddUpdateUsers(User user);
        public void DeleteUser(int id);
        List <UserRole> GetRolesList();
        
    }
}
