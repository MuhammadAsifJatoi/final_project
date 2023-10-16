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
    public class PostRepository : IPost
    {
        private readonly AsifBlogDbContext _db;
        public PostRepository(AsifBlogDbContext db)
        {
            _db = db;
        }
        //post method
       public List<Post> GetPosts
        {
            get { return _db.Posts.Include(x => x.Category).Include(x=>x.PostStatus).ToList(); }
        }
       public Post GetPost(int id)
        {
            return _db.Posts.Where(x => x.Id.Equals(id)).Include(x => x.Category).Include(x => x.PostStatus).Include(x => x.User).FirstOrDefault();
        }
        public void CreatePost(Post post)
        {
            post.postedon = DateTime.UtcNow.AddHours(5);
            _db.Posts.Add(post);
            _db.SaveChanges();
        }
        public void UpdatePost(Post post)
        {
            _db.Posts.Update(post);
            _db.SaveChanges();
        }
        public void DeletePost(int id)
        {
            Post post = _db.Posts.Where(x=>x.Id==id).FirstOrDefault();
            _db.Posts.Remove(post);
            _db.SaveChanges();
        }
        public List<Post> GetActivePosts()
        {
            return _db.Posts.Where(x => x.PostStatus.Name.Equals("active")).ToList();
        }
        public List<Post> GetAuthorPost()
        {
            return _db.Posts.Where(x => x.User.UserRoleid == 2).ToList();
        }
        //categories method

        public List<Category> GetCategories()
        {
            return _db.Categories.ToList();
        }
        public Category GetCategory(int id)
        {
            return _db.Categories.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddUpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }
        public void DeleteCategory(int id)
        {
            Category category = _db.Categories.Where(x => x.Id==id).FirstOrDefault();
            _db.Remove(category);
            _db.SaveChanges();
        }
        //post status
        public List<PostStatus> GetPostStatuses()
        {
            return _db.PostStatuses.ToList();
        }
        public PostStatus GetPostStatus(int id)
        {
            return _db.PostStatuses.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddUpdatePostStatus(PostStatus postStatus)
        {
         _db.PostStatuses.Update(postStatus);
            _db.SaveChanges();
        }
        public void DeletePostStatus(int id)
        {
            PostStatus postStatus = _db.PostStatuses.Where(x => x.Id == id).FirstOrDefault();
            _db.Remove(postStatus);
            _db.SaveChanges();
        }
        //ReactionType
        public List<ReactionType> GetReactionTypes()
        {
            return _db.ReactionTypes.ToList();
        }
        public ReactionType GetReactionType(int id)
        {
            return _db.ReactionTypes.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddUpdateReactionType(ReactionType reactionType)
        {
            _db.ReactionTypes.Update(reactionType);
            _db.SaveChanges();
        }
        public void DeleteReactionType(int id)
        {
            ReactionType reactionType = _db.ReactionTypes.Where(x => x.Id == id).FirstOrDefault();
            _db.Remove(reactionType);
            _db.SaveChanges();
        }
        //post ReactionType
        public List<PostReaction> GetPostReactionTypes()
        {
           return _db.PostReactions.Include(x =>x.User).Include(x=>x.Post).Include(x=>x.ReactionType).ToList();
        }
        public void CreatePostReaction(PostReaction postReaction)
        {
            _db.PostReactions.Add(postReaction);
            _db.SaveChanges();
        }
        public PostReaction GetPostReaction(int id)
        {
            return _db.PostReactions.Where(x => x.Id == id).Include(x => x.User).Include(x => x.Post).Include(x => x.ReactionType).FirstOrDefault();
        }
        public void UpdatePostReaction(PostReaction postReaction)
        {
            _db.PostReactions.Update(postReaction);
            _db.SaveChanges();
        }
        public void DeletePostReaction(int id)
        {
            PostReaction postReaction = _db.PostReactions.Where(x=>x.Id == id).FirstOrDefault();
            _db.PostReactions.Remove(postReaction);
            _db.SaveChanges();
        }

    }
}
