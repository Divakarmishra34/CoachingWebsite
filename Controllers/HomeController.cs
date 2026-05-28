using Microsoft.AspNetCore.Mvc;

namespace CoachingWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
         public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Gallery()
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
public IActionResult About()
{
    return View();
}
public IActionResult NotFound()
{
    return View();
}
    }
}