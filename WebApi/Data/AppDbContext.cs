using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace WebApi.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}
