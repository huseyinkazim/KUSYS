using KUSYS.Data.DTO;
using KUSYS.Data.Entity;
using KUSYS.Data.Interface.Repository;
using KUSYS.Repository.DataBase;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace KUSYS.Repository
{
	public class StudentRepository : Repository<Student, int>, IStudentRepository
	{
		public StudentRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}

		public Student GetStudentByIdWithCourses(int id)
		{
			var entity = _dbContext.Set<Student>().AsNoTracking().Include(x => x.Course).Include(i => i.User).FirstOrDefault(i => i.Id == id);
			// _dbContext.Entry(entity).State = EntityState.Detached;

			return entity;
		}
		public Student GetStudentByIdWithUser(int id)
		{
			return _dbContext.Set<Student>().AsNoTracking().Include(x => x.User).FirstOrDefault(i => i.Id == id);
		}
		public List<Student> GetAllWithDep()
		{
			return _dbContext.Set<Student>().AsNoTracking().Include(x => x.User).Include(x => x.Course).Where(i => i.isDeleted == false).ToList();
		}
		public void UpdateStudentWithCourse(Student entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;

		}
		public Student GetStudentbyUserId(string id)
		{
			return _dbContext.Set<Student>().AsNoTracking().Include(i=>i.User).FirstOrDefault(i => i.User.Id == id);

		}


	}
}
