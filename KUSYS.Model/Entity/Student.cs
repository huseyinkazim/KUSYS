using KUSYS.Data.Entity.Base;
using Microsoft.AspNetCore.Identity;

namespace KUSYS.Data.Entity
{
    public class Student : EntityBase<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual IdentityUser User { get; set; }
        public string CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
