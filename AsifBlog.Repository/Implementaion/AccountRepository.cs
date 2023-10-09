using AsifBlog.Data;
using AsifBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AsifBlog.Repository.Implementaion
{
    public class AccountRepository : IUserAccount 
    {
        private readonly AsifBlogDbContext _db;
        public AccountRepository(AsifBlogDbContext db)
        {
            _db = db;
        }
        public User GetUserForLogin(string email,string password)
        {
            return _db.Users.Where(x => (string.IsNullOrEmpty(email) || x.Name.ToLower() == email.ToLower()) && x.Password.Equals(password)).FirstOrDefault();
            //return _db.Users.Where(x => x.EmailAdress.ToLower().Equals(email.ToLower()) && x.Password.Equals(password)).FirstOrDefault();
        }

        public User GetUserInfo(string accessToken)
        {
            return _db.Users.Where(x => x.AccesToken.Equals(accessToken)).FirstOrDefault();
        }

        public string Register(User user)
        {
            user.UserRoleid = 4;
            user.IsConfirm = true;
            user.joinedon=DateTime.UtcNow.AddHours(5);
            user.AccesToken=Guid.NewGuid().ToString()+DateTime.UtcNow.Ticks;
            _db.Users.Add(user);
            _db.SaveChanges();  
            return user.AccesToken+user.joinedon.Ticks.ToString();
            //throw new NotImplementedException();
        }
    }
}
