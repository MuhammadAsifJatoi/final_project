using AsifBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsifBlog.Repository
{
    public interface IPost
    {
        //post method
        public List<Post> GetPosts{ get; }
        public Post GetPost(int id);
        public void CreatePost (Post post);
        public void UpdatePost (Post post);
        public void DeletePost (int id);
        //category method
        public List<Category>GetCategories();
        public Category GetCategory(int id);
        public void AddUpdateCategory(Category category);
        public void DeleteCategory(int id);
        public List<Post> GetActivePosts();
        public List<Post> GetAuthorPost();
        //post status
        public List<PostStatus> GetPostStatuses();
        public PostStatus GetPostStatus(int id);
        public void AddUpdatePostStatus(PostStatus postStatus);
        public void DeletePostStatus(int id);
        //ReactionType
        public List<ReactionType> GetReactionTypes();
        public ReactionType GetReactionType(int id);
        public void AddUpdateReactionType(ReactionType reactionType);
        public void DeleteReactionType(int id);
        //postReaction 
        public List<PostReaction>GetPostReactionTypes();
        public void CreatePostReaction(PostReaction postReaction);
        public PostReaction GetPostReaction(int id);
        public void UpdatePostReaction(PostReaction postReaction);
        public void DeletePostReaction(int id);


    }
}
