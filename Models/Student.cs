using System.ComponentModel.DataAnnotations;

namespace Winterhold_College_Course_Registration_System.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [StringLength(30)]
        public string Major { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
