using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsManagement.Web.Models
{
    public class ClassRoom {
        [Key]
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Division { get; set; } // "A".."E"
        public virtual ICollection<ClassSubject> ClassSubjects { get; set; }
    }

    public class Student {
        [Key]
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClassRoomId { get; set; }
        [ForeignKey("ClassRoomId")] public virtual ClassRoom ClassRoom { get; set; }
    }

    public class Teacher {
        [Key]
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MaxWeeklyLectures { get; set; }
    }

    public class Subject {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ClassSubject {
        [Key]
        public int Id { get; set; }
        public int ClassRoomId { get; set; }
        public int SubjectId { get; set; }
        public int WeeklyPeriods { get; set; }
        [ForeignKey("ClassRoomId")] public virtual ClassRoom ClassRoom { get; set; }
        [ForeignKey("SubjectId")] public virtual Subject Subject { get; set; }
    }

    public class TeacherAvailability {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int DayOfWeek { get; set; }  // 1..7
        public int PeriodIndex { get; set; } // 1..N
    }

    public class TimetableSlot {
        [Key]
        public int Id { get; set; }
        public int ClassRoomId { get; set; }
        public int DayOfWeek { get; set; }
        public int PeriodIndex { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
        public int? RoomId { get; set; }
    }
}
