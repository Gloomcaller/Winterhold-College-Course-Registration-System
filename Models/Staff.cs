namespace Winterhold_College_Course_Registration_System.Models
{
    public class Staff : Person
    {
        public Role Role { get; set; }

        public DateTime HireDate { get; set; }

        public Department? Department { get; set; }

        public ICollection<Course> CoursesTaught { get; set; }
    }
}
