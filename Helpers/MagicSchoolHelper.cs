using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Winterhold_College_Course_Registration_System.Models;

namespace Winterhold_College_Course_Registration_System.Helpers
{
    public static class MagicSchoolHelper
    {
        public static IHtmlContent MagicSchoolIcon(this IHtmlHelper html, Department? department)
        {
            if (department == null)
                return new HtmlString("<span>[No School]</span>");

            string school = department.ToString();
            string iconPath = $"/pngs/{school.ToLower()}.png";
            string altText = school;

            string htmlString = $"<img src='{iconPath}' alt='{altText}' class='magic-school-icon' title='{school}' />";
            return new HtmlString(htmlString);
        }
    }
}
