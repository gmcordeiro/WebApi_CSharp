


namespace WebApi.Model.Employee {
	public interface IEmployeeRepository {

		void Add(Employee employee);

		List<Employee> GetAll();

	}
}
