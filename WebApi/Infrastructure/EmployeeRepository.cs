using WebApi.Model.Employee;

namespace WebApi.Infrastructure {
	public class EmployeeRepository : IEmployeeRepository {

		private readonly ConnectionContext _context = new();

		public void Add(Employee employee) {
			_context.Employees.Add(employee);
			_context.SaveChanges();
		}

		public Employee? Update(Employee employee) {

			var updateEmployee = _context.Employees.Update(employee).Entity;
			_context.SaveChanges();

			return updateEmployee;

		}

		public List<Employee?> GetAll() {
			return [.. _context.Employees];
		}

		public Employee? GetById(int id) {
			return _context.Employees.FirstOrDefault(e => e.Id == id) ?? null;
		}

		public void Delete(Employee employee) {
			_context.Employees.Remove(employee);
			_context.SaveChanges();
		}
	}
}
