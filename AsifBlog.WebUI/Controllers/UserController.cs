using AsifBlog.Model;
using AsifBlog.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AsifBlog.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        //userRole
        [Admin]
        [HttpGet]
        public IActionResult UserRoles()
        {
            return View(_user.GetUserRoles());
        }
        [Admin]
        [HttpGet]
        public IActionResult AddUpdateUserRole(int id = 0)
        {
            return View(_user.GetRole(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult AddUpdateUserRole(UserRole userRole)
        {
            _user.AddUpdateUserRole(userRole);
            return RedirectToAction("UserRoles");
        }
        [HttpGet]
        public IActionResult DeleteRole(int id)
        {
            _user.DeleteRole(id);
            return RedirectToAction("UserRoles");
        }
        //user
        [Admin]
        [HttpGet]
        public IActionResult Users()
        {
            return View(_user.GetUsers());
        }
        [Admin]
        [HttpGet]
        public IActionResult AddUpdateUser(int id = 0)
        {
            if (id == 0)
            {
                return RedirectToAction(" Register");
            }
            ViewBag.AllRoles= new SelectList(_user.GetRolesList().ToList(),"Id","Name");
            return View(_user.GetUser(id));
        }
        [Admin]
        [HttpPost]
        public IActionResult AddUpdateUser(User user)
        {
            _user.AddUpdateUsers(user);
            return RedirectToAction("Users");
        }
        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            _user.DeleteUser(id);
            return RedirectToAction("Users");
        }




    }
}
