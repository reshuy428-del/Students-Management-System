using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using StudentsManagement.Web.Models;

namespace StudentsManagement.Web.Services
{
    public class TimetableResult {
        public int AssignedCount { get; set; }
        public int UnassignedCount { get; set; }
    }

    public class TimetableGeneratorService
    {
        // Very simple greedy generator: creates events for each ClassSubject according to WeeklyPeriods
        // and assigns them to the first available slot where the class and a teacher are free.
        public TimetableResult GenerateSimpleTimetable(int daysPerWeek, int periodsPerDay)
        {
            using (var db = new ApplicationDbContext())
            {
                // Build events
                var classSubjects = db.ClassSubjects.Include(cs => cs.Subject).Include(cs => cs.ClassRoom).ToList();
                var teachers = db.Teachers.ToList();

                var events = new List<(int classId, int subjectId, int remaining)>(capacity: classSubjects.Count);
                foreach (var cs in classSubjects)
                {
                    for (int i = 0; i < cs.WeeklyPeriods; i++)
                        events.Add((cs.ClassRoomId, cs.SubjectId, 1));
                }

                // Initialize empty timetable slots if not exists
                var totalSlotsPerClass = daysPerWeek * periodsPerDay;
                var classes = db.Classes.ToList();
                foreach (var c in classes)
                {
                    for (int d = 1; d <= daysPerWeek; d++)
                    {
                        for (int p = 1; p <= periodsPerDay; p++)
                        {
                            if (!db.TimetableSlots.Any(s => s.ClassRoomId == c.Id && s.DayOfWeek == d && s.PeriodIndex == p))
                            {
                                db.TimetableSlots.Add(new TimetableSlot { ClassRoomId = c.Id, DayOfWeek = d, PeriodIndex = p });
                            }
                        }
                    }
                }
                db.SaveChanges();

                // Simple assignment: for each event, find first empty slot for that class and pick a random teacher
                int assigned = 0;
                int unassigned = 0;
                var random = new Random(0);
                var teacherIds = teachers.Select(t => t.Id).ToList();

                foreach (var ev in events)
                {
                    var slot = db.TimetableSlots.FirstOrDefault(s => s.ClassRoomId == ev.classId && s.SubjectId == null);
                    if (slot == null)
                    {
                        unassigned++;
                        continue;
                    }

                    // Pick a teacher that isn't already assigned at that slot time
                    var candidateTeacher = teacherIds.OrderBy(x => random.Next()).FirstOrDefault(tid => !db.TimetableSlots.Any(s => s.DayOfWeek == slot.DayOfWeek && s.PeriodIndex == slot.PeriodIndex && s.TeacherId == tid));
                    if (candidateTeacher == 0)
                    {
                        unassigned++;
                        continue;
                    }

                    slot.SubjectId = ev.subjectId;
                    slot.TeacherId = candidateTeacher;
                    db.Entry(slot).State = EntityState.Modified;
                    assigned++;
                }

                db.SaveChanges();
                return new TimetableResult { AssignedCount = assigned, UnassignedCount = unassigned };
            }
        }
    }
}
