using Microsoft.AspNetCore.Mvc;
using Winterhold_College_Course_Registration_System.Data;
using Winterhold_College_Course_Registration_System.Models;

namespace Winterhold_College_Course_Registration_System.Controllers
{
    public class StudentController : Controller
    {
        private readonly CollegeDbContext _context;

        public StudentController(CollegeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Enroll()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enroll(string Name, string Email, string BirthDate, int Major)
        {
            System.Diagnostics.Debug.WriteLine($"FORM SUBMITTED: {Name}, {Email}, {BirthDate}, {Major}");

            try
            {
                var student = new Student
                {
                    Name = Name,
                    Email = Email,
                    BirthDate = DateTime.Parse(BirthDate),
                    Major = (Department)Major,
                    EnrollmentDate = DateTime.Now,
                    Grade = GradeLevel.Novice
                };

                _context.Students.Add(student);
                _context.SaveChanges();

                System.Diagnostics.Debug.WriteLine("STUDENT SAVED SUCCESSFULLY!");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"STACK: {ex.StackTrace}");

                return View();
            }
        }
    }
}