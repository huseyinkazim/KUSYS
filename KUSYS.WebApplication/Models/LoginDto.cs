namespace KUSYS.WebApplication.Models
{
    public class LoginDto
    {
        public string Id{ get; set; }
        public string UserName{ get; set; }
        public string Password{ get; set; }
        public bool RememberLogin { get; set; }
    }
}
