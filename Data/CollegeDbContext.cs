using Microsoft.EntityFrameworkCore;
using Winterhold_College_Course_Registration_System.Models;

namespace Winterhold_College_Course_Registration_System.Data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Enum to string conversions ---
            modelBuilder.Entity<Staff>()
                .Property(s => s.Role)
                .HasConversion<string>()
                .HasMaxLength(30);

            modelBuilder.Entity<Staff>()
                .Property(s => s.Department)
                .HasConversion<string>()
                .HasMaxLength(30);

            modelBuilder.Entity<Student>()
                .Property(s => s.Major)
                .HasConversion<string>()
                .HasMaxLength(30);

            modelBuilder.Entity<Student>()
                .Property(s => s.Grade)
                .HasConversion<string>()
                .HasMaxLength(30);

            modelBuilder.Entity<Course>()
                .Property(c => c.Department)
                .HasConversion<string>()
                .HasMaxLength(30);

            modelBuilder.Entity<Course>()
                .Property(c => c.GradeLevel)
                .HasConversion<string>()
                .HasMaxLength(30);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Grade)
                .HasConversion<string>()
                .HasMaxLength(30);

            // --- Relationships ---
            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(s => s.CoursesTaught)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Prevent duplicate enrollment (same student in same course) ---
            modelBuilder.Entity<Enrollment>()
                .HasIndex(e => new { e.StudentId, e.CourseId })
                .IsUnique();

            // --- Seed data ---
            modelBuilder.Entity<Staff>().HasData(
                new Staff { Id = 1, Name = "Tolfdir", BirthDate = new DateTime(180, 3, 15), Role = Role.Professor, Department = Department.Alteration, HireDate = new DateTime(4150, 6, 1) },
                new Staff { Id = 2, Name = "Faralda", BirthDate = new DateTime(220, 7, 22), Role = Role.Professor, Department = Department.Destruction, HireDate = new DateTime(4160, 9, 10) },
                new Staff { Id = 3, Name = "Drevis Neloren", BirthDate = new DateTime(250, 1, 5), Role = Role.Professor, Department = Department.Illusion, HireDate = new DateTime(4155, 2, 20) },
                new Staff { Id = 4, Name = "Colette Marence", BirthDate = new DateTime(270, 11, 30), Role = Role.Professor, Department = Department.Restoration, HireDate = new DateTime(4165, 4, 5) },
                new Staff { Id = 5, Name = "Phinis Gestor", BirthDate = new DateTime(200, 8, 14), Role = Role.Professor, Department = Department.Conjuration, HireDate = new DateTime(4158, 7, 18) },
                new Staff { Id = 6, Name = "Sergius Turrianus", BirthDate = new DateTime(260, 5, 9), Role = Role.Professor, Department = Department.Enchanting, HireDate = new DateTime(4162, 11, 3) },
                new Staff { Id = 7, Name = "Savos Aren", BirthDate = new DateTime(150, 4, 12), Role = Role.Archmage, Department = null, HireDate = new DateTime(4140, 1, 1) },
                new Staff { Id = 8, Name = "Mirabelle Ervine", BirthDate = new DateTime(240, 6, 28), Role = Role.Assistant, Department = Department.Restoration, HireDate = new DateTime(4168, 3, 15) },
                new Staff { Id = 9, Name = "Urag gro-Shub", BirthDate = new DateTime(210, 9, 3), Role = Role.Librarian, Department = null, HireDate = new DateTime(4152, 8, 22) },
                new Staff { Id = 10, Name = "Arniel Gane", BirthDate = new DateTime(280, 2, 17), Role = Role.Assistant, Department = Department.Mysticism, HireDate = new DateTime(4170, 5, 7) }
            );

            modelBuilder.Entity<Course>().HasData(
                // Alteration
                new Course { Id = 1, CourseCode = "ALT-101", CourseName = "Foundations of Matter", Department = Department.Alteration, GradeLevel = GradeLevel.Novice, Credits = 2, ProfessorId = 1, IsActive = true, Description = "Introduction to manipulating physical reality through Alteration magic." },
                new Course { Id = 2, CourseCode = "ALT-201", CourseName = "Flesh Spells and Warding", Department = Department.Alteration, GradeLevel = GradeLevel.Apprentice, Credits = 3, ProfessorId = 1, IsActive = true, Description = "Defensive Alteration techniques including Oakflesh and Stoneflesh." },
                new Course { Id = 3, CourseCode = "ALT-401", CourseName = "Paralysis and Transmutation", Department = Department.Alteration, GradeLevel = GradeLevel.Expert, Credits = 5, ProfessorId = 1, IsActive = true, Description = "Advanced transmutation and paralysis spells for seasoned mages." },
                // Destruction
                new Course { Id = 4, CourseCode = "DES-101", CourseName = "Elemental Theory", Department = Department.Destruction, GradeLevel = GradeLevel.Novice, Credits = 2, ProfessorId = 2, IsActive = true, Description = "Understanding fire, frost, and shock as magical forces." },
                new Course { Id = 5, CourseCode = "DES-201", CourseName = "Flame and Frost Casting", Department = Department.Destruction, GradeLevel = GradeLevel.Apprentice, Credits = 3, ProfessorId = 2, IsActive = true, Description = "Practical casting of Flames, Frostbite, and Sparks." },
                new Course { Id = 6, CourseCode = "DES-501", CourseName = "Incineration Arts", Department = Department.Destruction, GradeLevel = GradeLevel.Master, Credits = 6, ProfessorId = 2, IsActive = true, Description = "Master-level Destruction magic. Firestorm, Blizzard, Thunder." },
                // Illusion
                new Course { Id = 7, CourseCode = "ILL-101", CourseName = "The Quiet Mind", Department = Department.Illusion, GradeLevel = GradeLevel.Novice, Credits = 2, ProfessorId = 3, IsActive = true, Description = "Introduction to mind-affecting magic and the theory of illusion." },
                new Course { Id = 8, CourseCode = "ILL-301", CourseName = "Invisibility and Muffle", Department = Department.Illusion, GradeLevel = GradeLevel.Adept, Credits = 4, ProfessorId = 3, IsActive = true, Description = "Concealment spells and sensory manipulation." },
                // Restoration
                new Course { Id = 9, CourseCode = "RES-101", CourseName = "Healing Fundamentals", Department = Department.Restoration, GradeLevel = GradeLevel.Novice, Credits = 2, ProfessorId = 4, IsActive = true, Description = "Basic healing spells, ward theory, and the ethics of Restoration." },
                new Course { Id = 10, CourseCode = "RES-301", CourseName = "Ward Mastery", Department = Department.Restoration, GradeLevel = GradeLevel.Adept, Credits = 4, ProfessorId = 4, IsActive = true, Description = "Advanced ward spells and magical resistance techniques." },
                // Conjuration
                new Course { Id = 11, CourseCode = "CON-101", CourseName = "Planes of Oblivion", Department = Department.Conjuration, GradeLevel = GradeLevel.Novice, Credits = 2, ProfessorId = 5, IsActive = true, Description = "Survey of Daedric realms and the theory of conjuration." },
                new Course { Id = 12, CourseCode = "CON-201", CourseName = "Summoning Familiars", Department = Department.Conjuration, GradeLevel = GradeLevel.Apprentice, Credits = 3, ProfessorId = 5, IsActive = true, Description = "Practical summoning of Atronachs and bound weapons." },
                // Enchanting
                new Course { Id = 13, CourseCode = "ENC-101", CourseName = "Soul Gems and Essences", Department = Department.Enchanting, GradeLevel = GradeLevel.Novice, Credits = 2, ProfessorId = 6, IsActive = true, Description = "Introduction to soul gems, soul trapping, and magical essences." },
                new Course { Id = 14, CourseCode = "ENC-301", CourseName = "Weapon Enchantment", Department = Department.Enchanting, GradeLevel = GradeLevel.Adept, Credits = 4, ProfessorId = 6, IsActive = true, Description = "Imbuing weapons with elemental and utility enchantments." },
                // Mysticism
                new Course { Id = 15, CourseCode = "MYS-201", CourseName = "Magical Theory", Department = Department.Mysticism, GradeLevel = GradeLevel.Apprentice, Credits = 3, ProfessorId = 10, IsActive = true, Description = "The study of magic as a fundamental force of the universe." },
                new Course { Id = 16, CourseCode = "MYS-401", CourseName = "Telekinesis and Detection", Department = Department.Mysticism, GradeLevel = GradeLevel.Expert, Credits = 5, ProfessorId = 10, IsActive = true, Description = "Advanced applications of Mysticism in field and laboratory." },
                // Management
                new Course { Id = 17, CourseCode = "MGT-101", CourseName = "College History and Lore", Department = Department.Management, GradeLevel = GradeLevel.Novice, Credits = 2, ProfessorId = 7, IsActive = true, Description = "Survey of the College's history from the First Era to the present." },
                new Course { Id = 18, CourseCode = "MGT-201", CourseName = "Arcane Administration", Department = Department.Management, GradeLevel = GradeLevel.Apprentice, Credits = 3, ProfessorId = 8, IsActive = true, Description = "Managing magical institutions, resources, and student records." }
            );
        }
    }
}

