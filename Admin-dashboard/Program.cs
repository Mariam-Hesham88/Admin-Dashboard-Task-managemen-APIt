using BLL.Services.Interfaces;
using BLL.Services;
using DAL.Data;
using DAL.Interfaces;
using DAL.Repositoies;
using BLL.DTOs;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DAL.Entites;
using Microsoft.AspNetCore.Identity;

namespace Admin_dashboard
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			#region JWT
			//JWT
			//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			//	.AddJwtBearer(options =>
			//	{
			//		options.TokenValidationParameters = new TokenValidationParameters
			//		{
			//			ValidateIssuer = true,
			//			ValidateAudience = true,
			//			ValidateLifetime = true,
			//			ValidateIssuerSigningKey = true,
			//			ValidIssuer = builder.Configuration["JWT:Issuer"],
			//			ValidAudience = builder.Configuration["JWT:Audience"],
			//			IssuerSigningKey = new SymmetricSecurityKey(
			//				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
			//			)
			//		};
			//	});
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
				};
			});

			#endregion

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			builder.Services.AddScoped<ITaskItemsRepository, TaskItemsRepository>();
			builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
			//builder.Services.AddScoped<IAdminRepository, IAdminRepository>();

			builder.Services.AddScoped<IDepartmentService, DepartmentService>();
			builder.Services.AddScoped<ITaskItemService, TaskItemService>();
			builder.Services.AddScoped<IEmployeeService, EmployeeService>();
			builder.Services.AddScoped<IDashboardService, DashboardService>();
			builder.Services.AddScoped<IAuthService, AuthService>();

			//Mapping ==> AutoMapper
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			builder.Services.AddControllers();
			builder.Services.AddValidatorsFromAssemblyContaining<Program>();

			//Identity
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();




			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();


			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

				string[] roles = { "Admin" };

				foreach (var role in roles)
				{
					var roleExist = roleManager.RoleExistsAsync(role).Result;
					if (!roleExist)
					{
						roleManager.CreateAsync(new IdentityRole(role)).Wait();
					}
				}
			}


			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication(); // Before UseAuthorization
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
