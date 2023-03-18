namespace KUSYS.WebApplication.Models
{
	public class TokenDto
	{
		public static string TokenStatic;

		private string _token;
		public string Token
		{
			get
			{
				return _token;
			}

			set
			{
				if (_token != value)
				{
					_token = value;
					TokenDto.TokenStatic = value;
				}
			}
		}
	}
}
