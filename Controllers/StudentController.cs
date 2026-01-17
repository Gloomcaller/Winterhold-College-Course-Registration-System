using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            ViewBag.Courses = new SelectList(_context.Courses.Where(c => c.IsActive), "Id", "CourseName");
            ViewBag.Majors = new SelectList(Enum.GetValues<Department>());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Enroll(Student student, int[] selectedCourses)
        {
            if (ModelState.IsValid)
            {
                student.EnrollmentDate = DateTime.Now;
                _context.Students.Add(student);
                _context.SaveChanges();

                foreach (var courseId in selectedCourses)
                {
                    var enrollment = new Enrollment
                    {
                        StudentId = student.Id,
                        CourseId = courseId,
                        EnrollmentDate = DateTime.Now
                    };
                    _context.Enrollments.Add(enrollment);
                }
                _context.SaveChanges();

                return RedirectToAction("Success", new { id = student.Id });
            }

            ViewBag.Courses = new SelectList(_context.Courses.Where(c => c.IsActive), "Id", "CourseName");
            ViewBag.Majors = new SelectList(Enum.GetValues<Department>());
            return View(student);
        }

        public IActionResult Success(int id)
        {
            var student = _context.Students.Find(id);
            return View(student);
        }
    }
}