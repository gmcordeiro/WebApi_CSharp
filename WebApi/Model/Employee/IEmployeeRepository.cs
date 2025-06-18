


namespace WebApi.Model.Employee {
	public interface IEmployeeRepository {

		void Add(Employee employee);

		List<Employee?> GetAll();

		Employee? GetById(int id);

		Employee? Update(Employee employee);

		void Delete(Employee employee);

	}
}
