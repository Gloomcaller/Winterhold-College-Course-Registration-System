using System.ComponentModel.DataAnnotations;

namespace Winterhold_College_Course_Registration_System.Models
{
    public class Student : Person
    {
        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public Department? Major { get; set; }

        public GradeLevel? Grade { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
