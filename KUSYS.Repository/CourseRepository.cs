using KUSYS.Data.Entity;
using KUSYS.Data.Interface.Repository;
using KUSYS.Repository.DataBase;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Repository
{
    public class CourseRepository : Repository<Course, string>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Course GetCourseByIdWithStudent(string Id)
        {
            return _dbContext.Set<Course>().AsNoTracking().Include(x => x.Students).FirstOrDefault(i => i.Id == Id);
        }
    }
}
