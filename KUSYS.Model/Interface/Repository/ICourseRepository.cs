using KUSYS.Data.Entity;

namespace KUSYS.Data.Interface.Repository
{
    public interface ICourseRepository : IRepository<Course,string>
    {
        Course GetCourseByIdWithStudent(string Id);
    }
}
