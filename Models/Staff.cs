using System.ComponentModel.DataAnnotations;

namespace Winterhold_College_Course_Registration_System.Models
{
    public class Staff
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Role { get; set; }

        public DateTime HireDate { get; set; }

        [StringLength(30)]
        public string Department { get; set; }

        public ICollection<Course> CoursesTaught { get; set; }
    }
}
