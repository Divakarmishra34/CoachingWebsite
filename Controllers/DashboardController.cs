
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoachingWebsite.Data;

namespace CoachingWebsite.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalStudents = _context.Students.Count();
            ViewBag.TotalCourses = _context.Students
    .Select(x => x.Course)
    .Distinct()
    .Count();
                                      ViewBag.RecentStudents = _context.Students
    .OrderByDescending(x => x.Id)
    .Take(5)
    .ToList();
    ViewBag.MathsCount = _context.Students
    .Count(x => x.Course == "Maths");

ViewBag.ScienceCount = _context.Students
    .Count(x => x.Course == "Science");

ViewBag.EnglishCount = _context.Students
    .Count(x => x.Course == "English");
    ViewBag.PresentStudents = _context.Students
    .Count(x => x.IsPresent);

ViewBag.AbsentStudents = _context.Students
    .Count(x => !x.IsPresent);
    ViewBag.FeesPaidStudents = _context.Students
    .Count(x => x.FeesPaid);

ViewBag.FeesPendingStudents = _context.Students
    .Count(x => !x.FeesPaid);
    ViewBag.TotalTeachers =
    _context.Teachers.Count();
            return View();
        }
        
        
    }
}