using KUSYS.Data.Interface.Repository;


namespace KUSYS.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        int Commit();
        Task<int> CommitAsync();

    }

}
