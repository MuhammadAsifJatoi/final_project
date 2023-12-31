﻿using AsifBlog.Model;
using AsifBlog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AsifBlog.WebUI.Controllers
{
    public class CommonController : Controller
    {
        private readonly IUserAccount _account;
        public CommonController(IUserAccount account)
        {
            _account = account;
        }
        public User GetUser(HttpContext context)
        {
            string cookie = context.Request.Cookies["user-access-token"];
            if (cookie != null)
            {
               User user = _account.GetUserInfo(cookie);
                if (user != null)
                {
                    return user;
                }
            }
            return null;
               
        }
    }
}
