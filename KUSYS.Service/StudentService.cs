using AutoMapper;
using KUSYS.Data.DTO;
using KUSYS.Data.Entity;
using KUSYS.Data.Interface;
using KUSYS.Data.Interface.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using static Azure.Core.HttpHeader;

namespace KUSYS.Service
{
	public class StudentService : IStudentService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<IdentityUser> _userManager;
		public StudentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_userManager = userManager;
		}

		public ServiceResponse<StudentDTO> Add(StudentDTO dtoObject)
		{
			Student entity = _mapper.Map<Student>(dtoObject);

			_unitOfWork.Students.Add(entity);
			_unitOfWork.Commit();
			var studentUser = new IdentityUser($"{entity.FirstName}.{entity.LastName}");
			var result = _userManager.CreateAsync(studentUser, "Asd!23").Result;
			IdentityResult roleResult;
			if (result.Succeeded)
			{
				roleResult = _userManager.AddToRoleAsync(studentUser, string.IsNullOrEmpty(dtoObject.RoleId) ? "US-4" : dtoObject.RoleId).Result;
				entity.User = studentUser;
				_unitOfWork.Students.Update(entity);
				_unitOfWork.Commit();
			}
			return new ServiceResponse<StudentDTO>(dtoObject);
		}
		public ServiceResponse<StudentDTO> Update(StudentDTO dtoObject)
		{

			var entity = _unitOfWork.Students.GetById(dtoObject.Id);
			entity.CourseId = dtoObject.CourseId;

			_unitOfWork.Students.Update(entity);

			var res = _unitOfWork.Commit();

			return new ServiceResponse<StudentDTO>(dtoObject);
		}
		public ServiceResponse<StudentDTO> UpdateWithCourse(StudentDTO dtoObject)
		{
			var entity = _unitOfWork.Students.GetById(dtoObject.Id);
			entity.CourseId = dtoObject.CourseId;
			var res = _unitOfWork.Commit();

			return new ServiceResponse<StudentDTO>(dtoObject);
		}
		public ServiceResponse<StudentDTO> Delete(StudentDTO dtoObject)
		{
			Student entity = _mapper.Map<Student>(dtoObject);

			_unitOfWork.Students.Delete(entity);
			_unitOfWork.Commit();

			return new ServiceResponse<StudentDTO>(dtoObject);
		}


		public ServiceResponse<List<StudentDTO>> GetAll()
		{
			var liste = _unitOfWork.Students.GetAllWithDep();
			List<StudentDTO> dtoList = _mapper.Map<List<StudentDTO>>(liste);
			dtoList.ForEach(i => i.RoleId = String.Join(",", _userManager.GetRolesAsync(i.User).Result));
			return new ServiceResponse<List<StudentDTO>>(dtoList);
		}

		public ServiceResponse<StudentDTO> GetById(int id)
		{
			var entity = _unitOfWork.Students.GetById(id);
			StudentDTO dtoList = _mapper.Map<StudentDTO>(entity);

			return new ServiceResponse<StudentDTO>(dtoList);
		}

		public ServiceResponse<StudentDTO> GetStudentByIdWithCourses(int id)
		{
			var entity = _unitOfWork.Students.GetStudentByIdWithCourses(id);
			StudentDTO dtoObject = _mapper.Map<StudentDTO>(entity);
			dtoObject.RoleId = String.Join(",", _userManager.GetRolesAsync(entity.User).Result);
			return new ServiceResponse<StudentDTO>(dtoObject);
		}
		public ServiceResponse<StudentDTO> GetStudentByIdWithUser(int Id)
		{
			var entity = _unitOfWork.Students.GetStudentByIdWithUser(Id);
			StudentDTO dtoObject = _mapper.Map<StudentDTO>(entity);

			return new ServiceResponse<StudentDTO>(dtoObject);
		}
		public ServiceResponse<StudentDTO> GetStudentbyUserId(string Id)
		{
			var entity = _unitOfWork.Students.GetStudentbyUserId(Id);
			StudentDTO dtoObject = _mapper.Map<StudentDTO>(entity);

			return new ServiceResponse<StudentDTO>(dtoObject);
		}

		public void Dispose()
		{
			_unitOfWork.Dispose();
		}
	}
}
