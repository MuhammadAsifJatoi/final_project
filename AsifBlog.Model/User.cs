using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsifBlog.Model
{
    public class User
    {
        public int Id { get; set; }
       
        public string Name{ get; set; }
       
        public string EmailAdress { get; set; }
      
        public string Password { get; set; }
    
        public string PhoneNumber { get; set; }
        
        public bool IsConfirm { get; set; }
        public DateTime joinedon { get; set; }
        public string   AccesToken { get; set; }
        public UserRole  userRole { get; set; }
        public virtual int UserRoleid { get; set; }

    }
}
