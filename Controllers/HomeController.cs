using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Winterhold_College_Course_Registration_System.Data;
using Winterhold_College_Course_Registration_System.Models;

namespace Winterhold_College_Course_Registration_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly CollegeDbContext _context;

        public HomeController(CollegeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.StudentCount = _context.Students.Count();
            ViewBag.CourseCount = _context.Courses.Count(c => c.IsActive);
            ViewBag.StaffCount = _context.Staff.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(string SenderName, string SenderEmail, string Subject, string Message)
        {
            if (string.IsNullOrWhiteSpace(SenderName) || string.IsNullOrWhiteSpace(SenderEmail)
                || string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(Message))
            {
                ViewBag.ContactError = "All fields are required.";
                return View();
            }

            // In a real implementation, send email here. For the mockup, we confirm receipt.
            ViewBag.ContactSuccess = $"Your message has been received, {SenderName}. A courier will respond to {SenderEmail} shortly.";
            return View();
        }

        public IActionResult Faculty()
        {
            var staff = _context.Staff
                .OrderBy(s => s.Role)
                .ThenBy(s => s.Name)
                .ToList();
            return View(staff);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

