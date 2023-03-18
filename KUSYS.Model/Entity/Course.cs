using KUSYS.Data.Entity.Base;

namespace KUSYS.Data.Entity
{
    public class Course : EntityBase<string>
    {
        public string CourseName { get; set; }

        public virtual ICollection<Student> Students{ get; set; }
    }
}
