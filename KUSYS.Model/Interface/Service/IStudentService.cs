using KUSYS.Data.DTO;
using Microsoft.AspNetCore.Identity;

namespace KUSYS.Data.Interface.Service
{
    public interface IStudentService : IService<StudentDTO, int>
    {
        ServiceResponse<StudentDTO> GetStudentByIdWithCourses(int Id);
        ServiceResponse<StudentDTO> GetStudentByIdWithUser(int Id);
		ServiceResponse<StudentDTO> GetStudentbyUserId(string Id);
	}
}
