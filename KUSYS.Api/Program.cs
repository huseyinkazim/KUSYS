using KUSYS.Repository.DataBase;
using KUSYS.Repository.DataBase.Seed;
using KUSYS.Data.Interface;
using KUSYS.Data.Interface.Repository;
using KUSYS.Data.Interface.Service;
using KUSYS.Repository;
using KUSYS.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using KUSYS.Api.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using KUSYS.Api.FilterAttributes;
using System.Text.Json.Serialization;
using KUSYS.Api.Middleware;
using FluentValidation;
using KUSYS.Api.Validation;
using KUSYS.Data.Entity;
using FluentValidation.AspNetCore;
using System.Reflection;
using KUSYS.Data.DTO;

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.AddEventLog();


// Add services to the container.
builder.Services.AddControllers()
	.AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
	.AddFluentValidation(options =>
	{
		// Validate child properties and root collection elements
		options.ImplicitlyValidateChildProperties = true;
		options.ImplicitlyValidateRootCollectionElements = true;

		// Automatic registration of validators in assembly
		options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
	});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
			   .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
		   .AddJwtBearer(options =>
		   {
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Keys:UserAuthSecretKey"])),
				   ValidIssuer = builder.Configuration["Keys:Issue"],
				   ValidAudience = builder.Configuration["Keys:Audience"],
				   ValidateLifetime = true
			   };
		   });
builder.Services.AddAuthorization();

builder.Services.AddLogging(options =>
{
	options.ClearProviders();
	options.AddProvider(new MyCustomLoggerProvider());

});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services
builder.Services.AddScoped<IValidator<StudentDTO>,StudentValidator>();

builder.Services.AddScoped<StudentValidationFilterAttribute>();
builder.Services.AddScoped<StudentSelfUserValidationFilterAttribute>();
builder.Services.AddScoped<StudentAddValidationFilterAttribute>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

builder.Services.AddAutoMapper(typeof(Program).Assembly);
var mapperConfig = new MapperConfiguration(mc =>
{
	mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<MyExceptionMiddleware>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();

	app.UseSwagger();
	app.UseSwaggerUI();
	
}
SeedDatabase.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

// Configure the HTTP request pipeline.
app.UseLogMiddleware();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
//app.UseEndpoints(routues => routues.MapControllerRoute("default", "api/{controller}/{action}/{id?}"));

app.MapControllers();


app.Run();
