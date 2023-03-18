using KUSYS.WebApplication.Models.Const;
using KUSYS.WebApplication.Models;

namespace KUSYS.UI.UIManager
{
    public interface IProxyManager
    {
        ServiceResponse<T> SendRequest<T>(string url, object data, HttpMethod method);
    }
}
