using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
