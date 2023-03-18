using KUSYS.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace KUSYS.Repository.DataBase.Seed
{
	public class SeedDatabase
	{
		private static ApplicationDbContext _context;
		private static RoleManager<IdentityRole> roleManager;
		private static UserManager<IdentityUser> userManager;
		public static void Initialize(IServiceProvider serviceProvider)
		{


			if (CheckSeed(serviceProvider))
				return;

			SeedRoles();
			SeedUsers();
			SeedCourses();
			SeedStudents();

		}
		private static bool CheckSeed(IServiceProvider serviceProvider)
		{
			_context = serviceProvider.GetRequiredService<ApplicationDbContext>();
			roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

			_context.Database.EnsureCreated();

			//var entity = _context.Set<Student>().Find(1);

			//entity.Courses = new List<Course> { };

			// Look for any students.
			return _context.Students.Any();

		}
		private static void SeedRoles()
		{
			var roles = new IdentityRole[]
			{
				new IdentityRole("US-1"),
				new IdentityRole("US-2"),
				new IdentityRole("US-3"),
				new IdentityRole("US-4"),
				new IdentityRole("Admin")
			};
			foreach (IdentityRole r in roles)
			{
				var result = roleManager.CreateAsync(r).Result;
			}
		}
		private async static void SeedUsers()
		{
			var admin = new IdentityUser("Admin");
			var result = userManager.CreateAsync(admin, "Asd!23").Result;
			IdentityResult roleResult;
			if (result.Succeeded)
			{
				roleResult = userManager.AddToRoleAsync(admin, "Admin").Result;
			}
		}
		private static void SeedCourses()
		{
			var courses = new Course[]
		  {
			new Course{Id="CSI101",CourseName="Introduction to Computer Science"},
			new Course{Id="CSI102",CourseName="Algorithms"},
			new Course{Id="MAT101",CourseName="Calculus"},
			new Course{Id="PHY101",CourseName="Physics"}
		  };
			foreach (Course c in courses)
			{
				_context.Courses.Add(c);
			}
			_context.SaveChanges();
		}
		private static void SeedStudents()
		{
			var students = new Student[]
			{
				new Student{FirstName="Carson",LastName="Alexander",BirthDate=DateTime.Parse("2005-09-01")},
				new Student{FirstName="Meredith",LastName="Alonso",BirthDate=DateTime.Parse("2002-09-01")}
			};
			IdentityUser studentUser;

			foreach (Student s in students)
			{
				_context.Students.Add(s);
				_context.SaveChanges();
				studentUser = new IdentityUser($"{s.FirstName}.{s.LastName}");
				var result = userManager.CreateAsync(studentUser, "Asd!23").Result;
				IdentityResult roleResult;
				if (result.Succeeded)
				{
					if (s.FirstName == "Carson")
						roleResult = userManager.AddToRolesAsync(studentUser, new List<string> { "US-1" }).Result;
					else
						roleResult = userManager.AddToRolesAsync(studentUser, new List<string> { "US-4" }).Result;
					s.User = studentUser;
					_context.Students.Update(s);
					_context.SaveChanges();
				}
			}
		}
	}
}
