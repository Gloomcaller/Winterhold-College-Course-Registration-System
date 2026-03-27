using System.ComponentModel.DataAnnotations;

namespace Winterhold_College_Course_Registration_System.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(10)]
        public string CourseCode { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public Department Department { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public int ProfessorId { get; set; }

        public int Credits { get; set; }

        [Required]
        public GradeLevel GradeLevel { get; set; }

        public bool IsActive { get; set; } = true;

        public Staff? Professor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
