using KUSYS.Data.DTO;
using KUSYS.Data.DTO.Base;
using System.Linq.Expressions;


namespace KUSYS.Data.Interface.Service
{
    public interface IService<TDTOEntity,TType> : IDisposable where TDTOEntity : DTOBase<TType>
    {
        ServiceResponse<TDTOEntity> Add(TDTOEntity dto);
        ServiceResponse<TDTOEntity> Update(TDTOEntity dto);
        ServiceResponse<TDTOEntity> Delete(TDTOEntity dto);
        ServiceResponse<TDTOEntity> GetById(TType id);
        ServiceResponse<List<TDTOEntity>> GetAll();
    }
}
