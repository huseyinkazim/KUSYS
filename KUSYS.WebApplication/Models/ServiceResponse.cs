using Newtonsoft.Json;

namespace KUSYS.WebApplication.Models
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }
    }
}
