using DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
	public class ApplicationDbContext:DbContext
	{
		public ApplicationDbContext()
		{
			
		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
		public DbSet<Admin> Admins { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<TaskItem> Tasks { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=SmartAdminDashboardDb;Trusted_Connection=True;MultipleActiveResultSets=true;Integrated security = true; TrustServerCertificate=True");
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TaskItem>()
				.HasOne(t => t.AssignedTo)
				.WithMany()
				.HasForeignKey(t => t.AssignedToId)
				.OnDelete(DeleteBehavior.Restrict);

		}

	}
}
