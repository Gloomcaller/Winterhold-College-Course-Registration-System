using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Winterhold_College_Course_Registration_System.Data;
using Winterhold_College_Course_Registration_System.Models;

namespace Winterhold_College_Course_Registration_System.Controllers
{
    public class CourseController : Controller
    {
        private readonly CollegeDbContext _context;

        public CourseController(CollegeDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? department)
        {
            var courses = _context.Courses
                .Include(c => c.Professor)
                .Where(c => c.IsActive)
                .AsQueryable();

            if (!string.IsNullOrEmpty(department) && Enum.TryParse<Department>(department, out var dept))
            {
                courses = courses.Where(c => c.Department == dept);
                ViewBag.SelectedDepartment = dept;
            }
            else
            {
                ViewBag.SelectedDepartment = null;
            }

            ViewBag.Departments = Enum.GetValues<Department>();
            return View(courses.ToList());
        }
    }
}