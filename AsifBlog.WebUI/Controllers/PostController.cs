using AsifBlog.Model;
using AsifBlog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace AsifBlog.WebUI.Controllers
{
    public class PostController : Controller
    {
        private readonly Repository.IPost _userPost;
        private readonly IUserAccount _account;
        public PostController(Repository.IPost userPost,IUserAccount account)
        {
            _userPost = userPost;
            _account = account;
        }
        //category action
        [Admin]
        [HttpGet]
        public IActionResult Categories()
        {
            return View(_userPost.GetCategories());
        }
        [Admin]
        [HttpGet]
        public IActionResult AddupdateCategory(int id = 0)
        {
            return View(_userPost.GetCategory(id));
        }

        [Admin]
        [HttpPost]
        public IActionResult AddupdateCategory(Category category)
        {
            _userPost.AddUpdateCategory(category);
            return RedirectToAction("Categories");
        }
        [HttpGet]
        [Admin]
        public IActionResult DeleteCategory(int id)
        {
            _userPost.DeleteCategory(id);
            return RedirectToAction("Categories");
        }
        //poststatus action
        [Admin]
        [HttpGet]
        public IActionResult PostStatus()
        {
            return View(_userPost.GetPostStatuses());
        }
        [Admin]
        [HttpGet]
        public IActionResult AddupdatePostStatus(int id = 0)
        {
            return View(_userPost.GetPostStatus(id));
        }

        [Admin]
        [HttpPost]
        public IActionResult AddupdatePostStatus(PostStatus poststatus)
        {

            _userPost.AddUpdatePostStatus(poststatus);
            return RedirectToAction("PostStatus");
        }
        [HttpGet]
        [Admin]
        public IActionResult DeletePostStatus(int id)
        {
            _userPost.DeletePostStatus(id);
            return RedirectToAction("PostStatus");
        }

        //post action
        [Admin]
        [HttpGet]
        public IActionResult Getposts()
        {
            return View(_userPost.GetPosts);
        }
        [HttpGet]
        [Admin]
        public IActionResult Detailpost(int id)
        {
            return View(_userPost.GetPost(id));
        }
        [Admin]
        [HttpGet]
        public IActionResult Createpost()
        {
            ViewBag.poststatus = new SelectList(_userPost.GetPostStatuses().ToList(),"Id","Name");
            ViewBag.Categories = new SelectList(_userPost.GetCategories().ToList(), "Id", "Name");
            return View(new Post());
        }
        [HttpPost]
        public IActionResult Createpost(Post post,IFormFile Postimg)
        {
            string imgpath = "";
            var extenction = "";
            IList<string> AllowfileExtension = new List<string> {".jpg",".jpeg",".png" };
            int maxcontentLength = 1024 * 1024 * 10; // 10mb
            if (Postimg != null && Postimg.Length>0)
            {
                extenction = Postimg.FileName.Substring(Postimg.FileName.LastIndexOf('.')).ToLower();
                if (Postimg.Length>maxcontentLength)
                {
                    ViewBag.Error = "File Size must be Less then 10Mb";
                }else if (!AllowfileExtension.Contains(extenction))
                {
                    ViewBag.Error = "Plese upload img of type .jpg,.jpeg,.png";
                }
                else
                {
                    string relativeimgpath=$"/img/posts/{post.Id}-{Path.GetFileNameWithoutExtension(Postimg.FileName)}-{DateTime.UtcNow.Ticks}.jpg";
                    string absoluteimgpath = Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot{relativeimgpath}");
                    using (var stream = new FileStream(absoluteimgpath, FileMode.Create))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            Postimg.CopyTo(memoryStream);
                            using(var img = Image.FromStream(memoryStream))
                            {
                             int width = img.Width;
                             int height = img.Height;
                                if (width >1500 ||height>1200)
                                {
                                    ViewBag.Error = "Plese upload img with dimension 700*500 or less";
                                }
                                else
                                {
                                    Postimg.CopyTo(stream);
                                    post.img = relativeimgpath;
                                    var user = new CommonController(_account).GetUser(HttpContext);
                                    post.UserId = user.Id;
                                    try
                                    {
                                        _userPost.CreatePost(post);
                                        return RedirectToAction("Getposts");
                                    }catch (Exception ex)
                                    {
                                        ViewBag.Error = "an error occurred while saving the post to the Database";
                                        return View(post);
                                    }
                                }
                            }
                        }
                    }
                }

            }
           
            return View();
           
        }
        [HttpGet]
        public IActionResult Updatepost(int id = 0)
        {
            ViewBag.poststatus = new SelectList(_userPost.GetPostStatuses().ToList(), "Id", "Name");
            ViewBag.Categories = new SelectList(_userPost.GetCategories().ToList(), "Id", "Name");
            return View(_userPost.GetPost(id));
        }
        [HttpPost]
        public IActionResult Updatepost(Post post)
        {
            _userPost.UpdatePost(post);
            return RedirectToAction("Getposts");
        }
        [HttpGet]
        public IActionResult Deletepost(int id)
        {
            _userPost.DeletePost(id);
            return RedirectToAction("Getposts");
        }
        // ReactionType

        [Admin]
        [HttpGet]
        public IActionResult ReactionType()
        {
            return View(_userPost.GetReactionTypes());
        }
        [HttpGet]
        [Admin]
        public IActionResult AdUpateReactionType(int id = 0)
        {
            return View(_userPost.GetReactionType(id));
        }
        [HttpPost]
        [Admin]
        public IActionResult AdUpateReactionType(ReactionType  reactionType)
        {
            _userPost.AddUpdateReactionType(reactionType);
            return RedirectToAction("ReactionType");
        }
        [HttpGet]
        public IActionResult DeleteReactionType(int id)
        {
            _userPost.DeleteReactionType(id);
            return RedirectToAction("ReactionType");
        }
        //postReaction
        [Admin]
        [HttpGet]
        public IActionResult PostReaction()
        {
            return View(_userPost.GetPostReactionTypes());
        }
        [Admin]
        [HttpGet]
        public IActionResult GetpostReactionType(int id = 0)
        {
            return View(_userPost.GetPostReaction(id));
        }
        [Admin]
        [HttpGet]
        public IActionResult CreatePostReactionType()
        {
            ViewBag.posts = new SelectList(_userPost.GetPosts.ToList(), "Id", "Title");
            ViewBag.Reactions = new SelectList(_userPost.GetReactionTypes().ToList(), "Id", "Name");
            return View(new PostReaction());
        }
        [Admin]
        [HttpPost]
        public IActionResult CreatePostReactionType(PostReaction postreaction)
        {
            User user = new CommonController(_account).GetUser(HttpContext);
            postreaction.userId = user.Id;
            _userPost.CreatePostReaction(postreaction);
            return RedirectToAction("PostReaction");
        }
        [Admin]
        [HttpGet]
        public IActionResult UpdatePostReactionType(int id = 0)
        {
            ViewBag.posts = new SelectList(_userPost.GetPostStatuses().ToList(), "Id", "Title");
            ViewBag.posts = new SelectList(_userPost.GetCategories().ToList(), "Id", "Name");
            return View(_userPost.GetPostReaction(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult UpdatePostReactionType(PostReaction postreaction)
        {
            _userPost.UpdatePostReaction(postreaction);
            return RedirectToAction("PostReaction");
            
        }
        [Admin]
        [HttpGet]
        public IActionResult DeletePostReactionType(int id)
        {
            _userPost.DeletePostReaction(id);
            return RedirectToAction("PostReaction");
        }

    }
}
