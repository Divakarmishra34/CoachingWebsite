using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CoachingWebsite.Models;
using CoachingWebsite.Data;

namespace CoachingWebsite.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _environment;


        public StudentController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;

            _environment = environment;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(
            Student student,
            IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string folder =
                    Path.Combine(
                        _environment.WebRootPath,
                        "images");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName =
                    Guid.NewGuid().ToString()
                    + "_"
                    + imageFile.FileName;

                string filePath =
                    Path.Combine(folder, fileName);

                using (var stream =
                    new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                student.ImagePath = "/images/" + fileName;
            }

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            ViewBag.Message =
                "Student Registered Successfully";

            return View();
        }


        // LIST + SEARCH + FILTER

        public IActionResult List(
            string search,
            string course)
        {
            var students =
                _context.Students.AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                students = students.Where(x =>
                    x.Name.Contains(search));
            }


            if (!string.IsNullOrEmpty(course))
            {
                students = students.Where(x =>
                    x.Course == course);
            }


            ViewBag.TotalStudents =
                students.Count();

            return View(students.ToList());
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student =
                _context.Students
                .FirstOrDefault(x => x.Id == id);

            return View(student);
        }


        [HttpPost]
        public IActionResult Edit(Student updatedStudent)
        {
            var student =
                _context.Students
                .FirstOrDefault(x =>
                    x.Id == updatedStudent.Id);

            if (student != null)
            {
                student.Name =
                    updatedStudent.Name;

                student.Email =
                    updatedStudent.Email;

                student.Course =
                    updatedStudent.Course;

                student.IsPresent =
                    updatedStudent.IsPresent;

                student.FeesPaid =
                    updatedStudent.FeesPaid;

                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }


        public IActionResult Delete(int id)
        {
            var student =
                _context.Students
                .FirstOrDefault(x => x.Id == id);

            if (student != null)
            {
                _context.Students.Remove(student);

                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }


        public IActionResult Details(int id)
        {
            var student =
                _context.Students
                .FirstOrDefault(x => x.Id == id);

            return View(student);
        }


        public IActionResult IdCard(int id)
        {
            var student =
                _context.Students
                .FirstOrDefault(x => x.Id == id);

            return View(student);
        }


        public IActionResult ExportExcel()
        {
            var students =
                _context.Students.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet =
                    workbook.Worksheets.Add("Students");

                worksheet.Cell(1, 1).Value =
                    "Name";

                worksheet.Cell(1, 2).Value =
                    "Email";

                worksheet.Cell(1, 3).Value =
                    "Course";

                int row = 2;

                foreach (var student in students)
                {
                    worksheet.Cell(row, 1).Value =
                        student.Name;

                    worksheet.Cell(row, 2).Value =
                        student.Email;

                    worksheet.Cell(row, 3).Value =
                        student.Course;

                    row++;
                }

                using (var stream =
                    new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    var content =
                        stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Students.xlsx");
                }
            }
        }
        public IActionResult Certificate(int id)
{
    var student = _context.Students
        .FirstOrDefault(x => x.Id == id);

    return View(student);
}
    }
}