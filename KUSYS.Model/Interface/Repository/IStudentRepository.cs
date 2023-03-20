using KUSYS.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace KUSYS.Data.Interface.Repository
{
    public interface IStudentRepository : IRepository<Student,int>
    {
        Student GetStudentByIdWithCourses(int id);
        Student GetStudentByIdWithUser(int id);
        List<Student> GetAllWithDep();
        Student GetStudentbyUserId(string id);

	}
}
