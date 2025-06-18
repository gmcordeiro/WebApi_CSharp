using WebApi.Model.Employee;

namespace WebApi.Infrastructure {
	public class EmployeeRepository : IEmployeeRepository {

		private readonly ConnectionContext _context = new();

		public void Add(Employee employee) {
			_context.Employees.Add(employee);
			_context.SaveChanges();
		}

		public List<Employee> GetAll() {
			return [.. _context.Employees];
		}

		public Employee GetById(int id) {
			return _context.Employees.FirstOrDefault(e => e.Id == id) ?? new Employee();
		}

		public void Delete(Employee employee) {
			_context.Employees.Remove(employee);
			_context.SaveChanges();
		}
	}
}
