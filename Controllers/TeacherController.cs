using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoachingWebsite.Data;
using CoachingWebsite.Models;

namespace CoachingWebsite.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(teacher);

                _context.SaveChanges();

                ViewBag.Message =
                    "Teacher Added Successfully";
            }

            return View();
        }


        public IActionResult List()
        {
            var teachers =
                _context.Teachers.ToList();

            return View(teachers);
        }


        public IActionResult Details(int id)
        {
            var teacher =
                _context.Teachers
                .FirstOrDefault(x => x.Id == id);

            return View(teacher);
        }
        [HttpGet]
public IActionResult Edit(int id)
{
    var teacher = _context.Teachers
        .FirstOrDefault(x => x.Id == id);

    return View(teacher);
}


[HttpPost]
public IActionResult Edit(Teacher updatedTeacher)
{
    var teacher = _context.Teachers
        .FirstOrDefault(x => x.Id == updatedTeacher.Id);

    if (teacher != null)
    {
        teacher.Name = updatedTeacher.Name;

        teacher.Subject = updatedTeacher.Subject;

        teacher.Email = updatedTeacher.Email;

        _context.SaveChanges();
    }

    return RedirectToAction("List");
}


public IActionResult Delete(int id)
{
    var teacher = _context.Teachers
        .FirstOrDefault(x => x.Id == id);

    if (teacher != null)
    {
        _context.Teachers.Remove(teacher);

        _context.SaveChanges();
    }

    return RedirectToAction("List");
}
    }
}