using KUSYS.Data.DTO;

namespace KUSYS.Data.Interface.Service
{
    public interface ICourseService : IService<CourseDTO,string>
    {
        ServiceResponse<CourseDTO> GetCourseByIdWithStudent(string id);
    }
}
