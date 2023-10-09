using AsifBlog.Model;
using AsifBlog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AsifBlog.WebUI.Controllers
{

    public class AccountController : Controller
    {
        private readonly Repository.IUserAccount _account;
        public AccountController(Repository.IUserAccount account)
        {
            _account = account;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection Data)
        {
            string email = Data["EmailAddress"];
            string password = Data["Password"];
            var dbUser = _account.GetUserForLogin(email, password);
            if (dbUser!= null) 
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddDays(30);
                Response.Cookies.Append("user-access-token", dbUser.AccesToken, options);
                return Redirect("/Home/Index");
            }
            ViewBag.Error = "Incorrect Email & Password";
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            string confitmationTkoen = _account.Register(user);
            if (string.IsNullOrEmpty(confitmationTkoen))
            {
                ViewBag.Error = "Error";
                return View();
            }
            else
            {
                CookieOptions options = new CookieOptions();
                options.Expires=DateTime.UtcNow.AddDays(30);
                Response.Cookies.Append("user-access-token", user.AccesToken, options);
                return Redirect("/Home/Index");
            }
            return View();

        }
        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user-access-token");

            return Redirect("/Home/Index");
        }
    }

}
