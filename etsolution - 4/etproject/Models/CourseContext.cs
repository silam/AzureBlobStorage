using System.Data.Entity;

namespace etproject
{
    public class CourseContext : DbContext
    {
        public DbSet<Course> CourseSet { get; set; }
    }
}
