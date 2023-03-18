using KUSYS.Data.DTO.Base;
using KUSYS.Data.Entity;
using Microsoft.AspNetCore.Identity;

namespace KUSYS.Data.DTO
{
    public class StudentDTO : DTOBase<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual IdentityUser User { get; set; }
        public string CourseId { get; set; }
        public CourseDTO Course { get; set; }
        public string RoleId { get; set; }
    }
}
