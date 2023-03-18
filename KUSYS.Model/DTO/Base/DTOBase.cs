namespace KUSYS.Data.DTO.Base
{
    public abstract class DTOBase<TType>
    {
        public TType Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool isDeleted { get; set; }
    }
}
