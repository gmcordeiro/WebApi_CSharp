using Microsoft.EntityFrameworkCore;
using WebApi.Model.Employee;

namespace WebApi.Infrastructure {
	public class ConnectionContext : DbContext {

		public DbSet<Employee> Employees { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseNpgsql(
				"Server=localhost;" +
				"Port=5432;" +
				"Database=postgres;" +
				"SearchPath=webapi;" +
				"User Id=postgres;" +
				"Password=1996;");
		}
	}
}
