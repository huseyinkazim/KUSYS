using AutoMapper;
using KUSYS.Data.DTO;
using KUSYS.Data.Entity;

namespace KUSYS.Api.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<CourseDTO, Course>();
            CreateMap<Student, StudentDTO>();
            CreateMap<StudentDTO, Student>();
            //CreateMap<StudentCourseDTO, StudentCourse>();
            //CreateMap<StudentCourse, StudentCourseDTO>();
        }
    }
}
