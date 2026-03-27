using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.Courses = _context.Courses
                .Include(c => c.Professor)
                .Where(c => c.IsActive)
                .OrderBy(c => c.GradeLevel)
                .ThenBy(c => c.Department)
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enroll(string Name, string Email, string BirthDate, int Major, List<int> SelectedCourseIds)
        {
            // Validate selected courses exist and are active
            var selectedCourses = _context.Courses
                .Where(c => SelectedCourseIds.Contains(c.Id) && c.IsActive)
                .ToList();

            // Check novice limit: max 2 novice-grade courses
            var noviceCount = selectedCourses.Count(c => c.GradeLevel == GradeLevel.Novice);
            if (noviceCount > 2)
            {
                ModelState.AddModelError("", "You may select at most 2 Novice-grade courses.");
                ViewBag.Courses = _context.Courses
                    .Include(c => c.Professor)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.GradeLevel)
                    .ThenBy(c => c.Department)
                    .ToList();
                ViewBag.EnrollmentError = "You may select at most 2 Novice-grade courses.";
                return View();
            }

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

                // Create enrollment records for selected courses
                foreach (var course in selectedCourses)
                {
                    _context.Enrollments.Add(new Enrollment
                    {
                        StudentId = student.Id,
                        CourseId = course.Id,
                        EnrollmentDate = DateTime.Now
                    });
                }
                _context.SaveChanges();

                return RedirectToAction("Success", new { studentId = student.Id });
            }
            catch (Exception ex)
            {
                ViewBag.EnrollmentError = "An error occurred during enrollment. Please try again.";
                System.Diagnostics.Debug.WriteLine($"Enrollment error: {ex.Message}");

                ViewBag.Courses = _context.Courses
                    .Include(c => c.Professor)
                    .Where(c => c.IsActive)
                    .OrderBy(c => c.GradeLevel)
                    .ThenBy(c => c.Department)
                    .ToList();
                return View();
            }
        }

        public IActionResult Success(int studentId)
        {
            var student = _context.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefault(s => s.Id == studentId);

            if (student == null)
                return RedirectToAction("Index", "Home");

            return View(student);
        }
    }
}
