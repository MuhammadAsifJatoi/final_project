using AsifBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AsifBlog.WebUI
{
    public class AdminAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accesstoken = context.HttpContext.Request.Cookies["user-access-token"];
            if (!string.IsNullOrEmpty(accesstoken))
            {
                AsifBlogDbContext db = context.HttpContext.RequestServices.GetRequiredService<AsifBlogDbContext>();
                var test = db.Users.Where(x=>x.AccesToken.Equals(accesstoken) && x.userRole.Name.Equals("Admin")).Any();
                if (!test)
                {
                    context.Result = new RedirectResult("/Account/Login");
                }
                
            }
            else
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

       
    }
}
