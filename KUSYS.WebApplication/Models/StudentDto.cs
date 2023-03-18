using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace KUSYS.WebApplication.Models
{
	public class StudentDto
	{
		public int Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		[BindProperty, DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }
        public string CourseId { get; set; }
        public CourseDTO Course { get; set; }
        public string RoleId { get; set; }

    }
}
