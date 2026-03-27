using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Winterhold_College_Course_Registration_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    GradeLevel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Staff_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "BirthDate", "Department", "HireDate", "Name", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(180, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alteration", new DateTime(4150, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tolfdir", "Professor" },
                    { 2, new DateTime(220, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Destruction", new DateTime(4160, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Faralda", "Professor" },
                    { 3, new DateTime(250, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Illusion", new DateTime(4155, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Drevis Neloren", "Professor" },
                    { 4, new DateTime(270, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restoration", new DateTime(4165, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colette Marence", "Professor" },
                    { 5, new DateTime(200, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Conjuration", new DateTime(4158, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phinis Gestor", "Professor" },
                    { 6, new DateTime(260, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enchanting", new DateTime(4162, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sergius Turrianus", "Professor" },
                    { 7, new DateTime(150, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(4140, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Savos Aren", "Archmage" },
                    { 8, new DateTime(240, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restoration", new DateTime(4168, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mirabelle Ervine", "Assistant" },
                    { 9, new DateTime(210, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(4152, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Urag gro-Shub", "Librarian" },
                    { 10, new DateTime(280, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mysticism", new DateTime(4170, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arniel Gane", "Assistant" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseCode", "CourseName", "Credits", "Department", "Description", "GradeLevel", "IsActive", "ProfessorId" },
                values: new object[,]
                {
                    { 1, "ALT-101", "Foundations of Matter", 2, "Alteration", "Introduction to manipulating physical reality through Alteration magic.", "Novice", true, 1 },
                    { 2, "ALT-201", "Flesh Spells and Warding", 3, "Alteration", "Defensive Alteration techniques including Oakflesh and Stoneflesh.", "Apprentice", true, 1 },
                    { 3, "ALT-401", "Paralysis and Transmutation", 5, "Alteration", "Advanced transmutation and paralysis spells for seasoned mages.", "Expert", true, 1 },
                    { 4, "DES-101", "Elemental Theory", 2, "Destruction", "Understanding fire, frost, and shock as magical forces.", "Novice", true, 2 },
                    { 5, "DES-201", "Flame and Frost Casting", 3, "Destruction", "Practical casting of Flames, Frostbite, and Sparks.", "Apprentice", true, 2 },
                    { 6, "DES-501", "Incineration Arts", 6, "Destruction", "Master-level Destruction magic. Firestorm, Blizzard, Thunder.", "Master", true, 2 },
                    { 7, "ILL-101", "The Quiet Mind", 2, "Illusion", "Introduction to mind-affecting magic and the theory of illusion.", "Novice", true, 3 },
                    { 8, "ILL-301", "Invisibility and Muffle", 4, "Illusion", "Concealment spells and sensory manipulation.", "Adept", true, 3 },
                    { 9, "RES-101", "Healing Fundamentals", 2, "Restoration", "Basic healing spells, ward theory, and the ethics of Restoration.", "Novice", true, 4 },
                    { 10, "RES-301", "Ward Mastery", 4, "Restoration", "Advanced ward spells and magical resistance techniques.", "Adept", true, 4 },
                    { 11, "CON-101", "Planes of Oblivion", 2, "Conjuration", "Survey of Daedric realms and the theory of conjuration.", "Novice", true, 5 },
                    { 12, "CON-201", "Summoning Familiars", 3, "Conjuration", "Practical summoning of Atronachs and bound weapons.", "Apprentice", true, 5 },
                    { 13, "ENC-101", "Soul Gems and Essences", 2, "Enchanting", "Introduction to soul gems, soul trapping, and magical essences.", "Novice", true, 6 },
                    { 14, "ENC-301", "Weapon Enchantment", 4, "Enchanting", "Imbuing weapons with elemental and utility enchantments.", "Adept", true, 6 },
                    { 15, "MYS-201", "Magical Theory", 3, "Mysticism", "The study of magic as a fundamental force of the universe.", "Apprentice", true, 10 },
                    { 16, "MYS-401", "Telekinesis and Detection", 5, "Mysticism", "Advanced applications of Mysticism in field and laboratory.", "Expert", true, 10 },
                    { 17, "MGT-101", "College History and Lore", 2, "Management", "Survey of the College's history from the First Era to the present.", "Novice", true, 7 },
                    { 18, "MGT-201", "Arcane Administration", 3, "Management", "Managing magical institutions, resources, and student records.", "Apprentice", true, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProfessorId",
                table: "Courses",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId_CourseId",
                table: "Enrollments",
                columns: new[] { "StudentId", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
