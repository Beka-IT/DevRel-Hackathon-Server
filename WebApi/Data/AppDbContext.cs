using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;
using WebApi.Entities;

namespace WebApi.Data
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }
		public DbSet<Entities.Task> Tasks { get; set; }
		public DbSet<Reference> References { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Company>()
				.HasMany(e => e.Employees)
				.WithOne(e => e.Company)
				.HasForeignKey(e => e.CompanyId)
				.IsRequired();

			modelBuilder.Entity<Company>()
				.HasMany(e => e.Projects)
				.WithOne(e => e.Company)
				.HasForeignKey(e => e.CompanyId)
				.IsRequired();

			modelBuilder.Entity<Project>()
				.HasMany(e => e.Tasks)
				.WithOne(e => e.Project)
				.HasForeignKey(e => e.ProjectId)
				.IsRequired();

            modelBuilder.Entity<Entities.Task>()
				.HasMany(e => e.Comments)
				.WithOne(e => e.Task)
				.HasForeignKey(e => e.TaskId)
				.IsRequired();

            modelBuilder.Entity<User>()
				.HasMany(e => e.Projects)
				.WithMany(e => e.Employees);
		}
	}
}
