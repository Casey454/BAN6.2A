using Microsoft.AspNetCore.Mvc;

namespace PresentationWebApp1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                //give an error to the user
                //block the user


                TempData["error"] = "Access Denied";

                return RedirectToAction("Index", "Home");
            }


            return View();
        }
    }
}
