using System.ComponentModel.DataAnnotations;

namespace KUSYS.Data.Entity.Base
{
    public abstract class EntityBase<TType>
    {
        [Key]
        public TType Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
