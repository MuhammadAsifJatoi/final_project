using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsifBlog.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string shortDescription { get; set; }
        public string img { get; set; }
        public string description { get; set; }
        public DateTime postedon { get; set; }
        public  Category Category { get; set; }
        public virtual int CategoryId { get; set; }
        public User User { get; set; }
        public virtual int UserId { get; set; }
        public PostStatus PostStatus { get; set; }
        public virtual int PostStatusId { get; set; }
    }
}
