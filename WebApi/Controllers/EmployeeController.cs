using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Employee;
using WebApi.ViewModel;

namespace WebApi.Controllers {

	[ApiController]
	[Route("api/v1/employee")]
	public class EmployeeController(IEmployeeRepository employeeRepository) : Controller {

		private readonly IEmployeeRepository _employeeRepository = employeeRepository;

		[HttpPost]
		public IActionResult Add(EmployeeViewModel employeeView) {
			var employee = new Employee(
				name: employeeView.Name,
				age: int.Parse(employeeView.Age),
				photo: null);

			_employeeRepository.Add(employee);
			return Ok();
		}

		[HttpGet]
		public IActionResult GetAll() {
			var employees = _employeeRepository.GetAll();
			var employeeViewModels = employees.Select(e => new EmployeeViewModel {
				Name = e.Name,
				Age = e.Age.ToString()
			}).ToList();
			return Ok(employees);
		}

	}
}
