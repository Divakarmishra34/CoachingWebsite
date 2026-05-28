using Microsoft.AspNetCore.Mvc;

namespace CoachingWebsite.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Science()
        {
            return View();
        }

        public IActionResult English()
        {
            return View();
        }
    }
}