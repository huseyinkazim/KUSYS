using KUSYS.Data.DTO.Base;

namespace KUSYS.Data.DTO
{
    public class CourseDTO : DTOBase<string>
    {
        public string CourseName { get; set; }

        public List<StudentDTO> Students { get; set; }
    }
}
