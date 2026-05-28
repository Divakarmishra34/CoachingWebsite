using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using System.Linq;

using CoachingWebsite.Data;
using CoachingWebsite.Models;

namespace CoachingWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        // CREATE ADMIN

        [AllowAnonymous]
        public IActionResult CreateAdmin()
        {
            var checkAdmin = _context.Admins
                .FirstOrDefault(x => x.Username == "admin");

            if (checkAdmin == null)
            {
                Admin admin = new Admin();

                admin.Username = "admin";

                admin.Password = "123";

                _context.Admins.Add(admin);

                _context.SaveChanges();
            }

            return Content("Admin Created Successfully");
        }


        // LOGIN PAGE

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // LOGIN POST

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(Admin admin)
        {
            var checkAdmin = _context.Admins
                .FirstOrDefault(x =>
                    x.Username == admin.Username &&
                    x.Password == admin.Password);

            if (checkAdmin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, admin.Username)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal);

                return RedirectToAction(
                    "Index",
                    "Dashboard");
            }

            ViewBag.Message =
                "Invalid Username Or Password";

            return View();
        }


        // LOGOUT

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }


        // ACCESS DENIED

        public IActionResult AccessDenied()
        {
            return View();
        }


        // PROFILE

        public IActionResult Profile()
        {
            ViewBag.Username = User.Identity.Name;

            return View();
        }


        // CHANGE PASSWORD PAGE

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }


        // CHANGE PASSWORD POST

        [HttpPost]
        public IActionResult ChangePassword(Admin admin)
        {
            var currentAdmin = _context.Admins
                .FirstOrDefault(x =>
                    x.Username == User.Identity.Name);

            if (currentAdmin != null)
            {
                if (admin.NewPassword ==
                    admin.ConfirmPassword)
                {
                    currentAdmin.Password =
                        admin.NewPassword;

                    _context.SaveChanges();

                    ViewBag.Message =
                        "Password Changed Successfully";
                }
                else
                {
                    ViewBag.Message =
                        "Passwords Do Not Match";
                }
            }

            return View();
        }
    }
}