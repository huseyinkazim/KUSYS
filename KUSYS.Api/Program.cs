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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KUSYS.Api.Controllers.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using KUSYS.Api.FilterAttributes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddEventLog();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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

#region Services
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

var app = builder.Build();
SeedDatabase.Initialize(app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseRouting();

app.UseAuthorization();


app.MapControllers();



app.Run();
