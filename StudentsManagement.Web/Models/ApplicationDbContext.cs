using System;
using System.Data.Common;
using System.Configuration;
using System.Data.Entity;
using Npgsql;
using StudentsManagement.Web.Models;

namespace StudentsManagement.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(CreateConnection(), true)
        {
            // Development-time initializer: create DB if it doesn't exist.
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        private static DbConnection CreateConnection()
        {
            // Prefer environment variables for configuration (Option 3).
            var host = Environment.GetEnvironmentVariable("PG_HOST");
            if (!string.IsNullOrEmpty(host))
            {
                int port = 5432;
                var portEnv = Environment.GetEnvironmentVariable("PG_PORT");
                if (!string.IsNullOrEmpty(portEnv)) Int32.TryParse(portEnv, out port);

                var builder = new NpgsqlConnectionStringBuilder
                {
                    Host = host,
                    Port = port,
                    Database = Environment.GetEnvironmentVariable("PG_DB") ?? "studentsdb",
                    Username = Environment.GetEnvironmentVariable("PG_USER") ?? "postgres",
                    Password = Environment.GetEnvironmentVariable("PG_PASSWORD") ?? "changeme",
                    Pooling = true,
                    MinPoolSize = 1,
                    MaxPoolSize = 100
                };

                return new NpgsqlConnection(builder.ConnectionString);
            }

            // Fallback to Web.config DefaultConnection
            var cfg = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (cfg != null && !string.IsNullOrEmpty(cfg.ConnectionString))
            {
                return new NpgsqlConnection(cfg.ConnectionString);
            }

            throw new InvalidOperationException("No database connection information found. Set PG_HOST/PG_DB/etc. environment variables or configure DefaultConnection in Web.config.");
        }

        public DbSet<ClassRoom> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<TeacherAvailability> TeacherAvailabilities { get; set; }
        public DbSet<TimetableSlot> TimetableSlots { get; set; }
    }
}
