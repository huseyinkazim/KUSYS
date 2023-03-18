using Newtonsoft.Json;

namespace KUSYS.WebApplication.Models
{
	public class CourseDTO
	{
		[JsonPropertyAttribute("id")] 
		public string CourseId { get; set; }
		public string CourseName { get; set; }
	}
}
