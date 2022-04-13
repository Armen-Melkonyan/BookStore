using Microsoft.AspNetCore.Mvc;

namespace MyTask.BookStore.Areas.Financial.Controllers
{
    [Area("financial")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Graph()
        {
            return View();
        }
    }
}
