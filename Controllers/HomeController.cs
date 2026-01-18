using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Winterhold_College_Course_Registration_System.Models;

namespace Winterhold_College_Course_Registration_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.StudentCount = 47;
            ViewBag.CourseCount = 32;
            ViewBag.StaffCount = 8;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "The College of Winterhold is Skyrim's oldest and most prestigious institution of magical learning.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Arcanaeum Library, Winterhold, Skyrim";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
