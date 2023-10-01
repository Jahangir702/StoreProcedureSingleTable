using Microsoft.AspNetCore.Mvc;

namespace _9th_Monthly_Exam.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
