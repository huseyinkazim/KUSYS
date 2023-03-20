using System.Text.Json;
using System.Text;
using KUSYS.WebApplication.Models;
using KUSYS.WebApplication.Models.Const;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Net.Http;

namespace KUSYS.UI.UIManager
{
	public class ProxyManager : IProxyManager
	{
		private const string GeneralError = "Bir hata oluştu";
		private readonly HttpClient _httpClient;

		public ProxyManager(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public ServiceResponse<T> SendRequest<T>(string url, object data, HttpMethod method)
		{
			HttpResponseMessage result = null;
			ServiceResponse<T> response;

			using (var request = new HttpRequestMessage(method, url))
			{
				if (data != null)
				{
					var serializeData = JsonConvert.SerializeObject(data);
					StringContent stringContent = new StringContent(serializeData, Encoding.UTF8, "application/json");
					request.Content = stringContent;
				}
				//todo:süresi biten token sıfırlanacak
				if (!string.IsNullOrEmpty(TokenDto.TokenStatic)) {
					request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TokenDto.TokenStatic);
				}

				result = _httpClient.SendAsync(request).Result;
			}

			var jsonString = result.Content.ReadAsStringAsync().Result;

			if (result.IsSuccessStatusCode)
				response = JsonConvert.DeserializeObject<ServiceResponse<T>>(jsonString);
			else
				response = new ServiceResponse<T>() { IsSuccess = false, Error = jsonString != string.Empty ? jsonString : GeneralError };

			return response;
		}

	}
}
