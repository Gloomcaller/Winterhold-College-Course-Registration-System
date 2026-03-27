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

        public IActionResult Course(string? department, string sort = "code")
        {
            var courses = _context.Courses
                .Include(c => c.Professor)
                .Where(c => c.IsActive)
                .AsQueryable();

            Department? selectedDept = null;
            if (!string.IsNullOrEmpty(department) && Enum.TryParse<Department>(department, out var dept))
            {
                courses = courses.Where(c => c.Department == dept);
                selectedDept = dept;
            }

            courses = sort switch
            {
                "name"       => courses.OrderBy(c => c.CourseName),
                "dept"       => courses.OrderBy(c => c.Department),
                "prof"       => courses.OrderBy(c => c.Professor!.Name),
                "credits"    => courses.OrderBy(c => c.Credits),
                "gradelevel" => courses.OrderBy(c => c.GradeLevel),
                _            => courses.OrderBy(c => c.CourseCode)
            };

            ViewBag.SelectedDepartment = selectedDept;
            ViewBag.Departments = Enum.GetValues<Department>();
            ViewBag.Sort = sort;

            return View(courses.ToList());
        }
    }
}

