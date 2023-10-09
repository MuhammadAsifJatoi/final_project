using AsifBlog.Data;
using AsifBlog.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsifBlog.Repository.Implementaion
{

    public class UserRepository : IUser
    {
        private readonly AsifBlogDbContext _user;
        public UserRepository(AsifBlogDbContext user)
        {
            _user = user;
        }
        //userRole
        List<UserRole> IUser.GetUserRoles()
        {
            //throw new NotImplementedException();
            return _user.UserRoles.ToList();
        }
        public UserRole GetRole(int id)
        {
            return _user.UserRoles.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddUpdateUserRole(UserRole userRole)
        {
            _user.UserRoles.Update(userRole);
            _user.SaveChanges();
        }
        public void DeleteRole(int id)
        {
         UserRole userRole = _user.UserRoles.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _user.Remove(userRole);
            _user.SaveChanges();
        }
        //users
        public List<User> GetUsers()
        {
            return _user.Users.Include(x => x.userRole).ToList();
        }
        public User GetUser(int id)
        {
            return _user.Users.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddUpdateUsers(User user)
        {
            _user.Users.Update(user);
            _user.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            User user = _user.Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _user.Remove(user);
            _user.SaveChanges();
        }
       public List<UserRole> GetRolesList()
        {
            return _user.UserRoles.ToList();
        }

    }

    
}
  