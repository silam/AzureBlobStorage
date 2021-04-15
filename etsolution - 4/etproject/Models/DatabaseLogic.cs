using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace etproject.Models
{
    public class DatabaseLogic
    {
        CourseContext ctx;
        public IQueryable<Course> GetAll()
        {
            ctx = new CourseContext();
            var courselst = from c in ctx.CourseSet
                            select c;
            return (courselst);
        }

        public Course GetCourseDetails(int id)
        {
            ctx = new CourseContext();
            var courseobj = ctx.CourseSet.FirstOrDefault(c =>c.Id == id);
            return courseobj;
        }

        public Course EditCourse(Course p_course)
        {
            ctx = new CourseContext();
            Course obj = ctx.CourseSet.FirstOrDefault(c => c.Id == p_course.Id);
            obj.rating = p_course.rating;
            ctx.SaveChanges();
            return obj;
        }

        public Course CreateCourse(Course p_course)
        {
            ctx = new CourseContext();
            ctx.CourseSet.Add(p_course);
            ctx.SaveChanges();
            return p_course;
        }

    }
}