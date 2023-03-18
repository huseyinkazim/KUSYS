using KUSYS.Data.Interface.Repository;
using KUSYS.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KUSYS.Repository.DataBase;
using KUSYS.Repository.DataBase.Seed;

namespace KUSYS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Students = new StudentRepository(_context);
            Courses = new CourseRepository(_context);
        }

        public IStudentRepository Students { get; private set; }

        public ICourseRepository Courses { get; private set; }

        public int Commit()
        {
            return _context.SaveChanges();
        }
        
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
       
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
