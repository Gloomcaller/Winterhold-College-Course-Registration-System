using System.ComponentModel.DataAnnotations;

namespace Winterhold_College_Course_Registration_System.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(10)]
        public string CourseCode { get; set; }

        [Required, StringLength(100)]
        public string CourseName { get; set; }

        [Required, StringLength(20)]
        public string Department { get; set; }

        public int ProfessorId { get; set; }

        public int Credits { get; set; }

        public bool IsActive { get; set; } = true;

        public Staff Professor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
