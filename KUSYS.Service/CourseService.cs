using AutoMapper;
using KUSYS.Data.DTO;
using KUSYS.Data.Entity;
using KUSYS.Data.Interface;
using KUSYS.Data.Interface.Service;

namespace KUSYS.Service
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ServiceResponse<CourseDTO> Add(CourseDTO dtoObject)
        {
            Course entity = _mapper.Map<Course>(dtoObject);

            _unitOfWork.Courses.Add(entity);
            _unitOfWork.Commit();

            return new ServiceResponse<CourseDTO>(dtoObject);
        }
        public ServiceResponse<CourseDTO> Update(CourseDTO dtoObject)
        {
            Course entity = _mapper.Map<Course>(dtoObject);

            _unitOfWork.Courses.Update(entity);
            _unitOfWork.Commit();

            return new ServiceResponse<CourseDTO>(dtoObject);
        }

        public ServiceResponse<CourseDTO> Delete(CourseDTO dtoObject)
        {
            Course entity = _mapper.Map<Course>(dtoObject);

            _unitOfWork.Courses.Delete(entity);
            _unitOfWork.Commit();

            return new ServiceResponse<CourseDTO>(dtoObject);
        }


        public ServiceResponse<List<CourseDTO>> GetAll()
        {
            var liste = _unitOfWork.Courses.FindAll(i => i.isDeleted == false).ToList();
            List<CourseDTO> dtoList = _mapper.Map<List<CourseDTO>>(liste);

            return new ServiceResponse<List<CourseDTO>>(dtoList);
        }

        public ServiceResponse<CourseDTO> GetById(string id)
        {
            var entity = _unitOfWork.Courses.GetById(id);
            CourseDTO dtoList = _mapper.Map<CourseDTO>(entity);

            return new ServiceResponse<CourseDTO>(dtoList);
        }

        public ServiceResponse<CourseDTO> GetCourseByIdWithStudent(string id)
        {
            var entity = _unitOfWork.Courses.GetCourseByIdWithStudent(id);
            CourseDTO dtoObject = _mapper.Map<CourseDTO>(entity);

            return new ServiceResponse<CourseDTO>(dtoObject);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
