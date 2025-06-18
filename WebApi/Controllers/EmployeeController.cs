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
			return Ok(employeeViewModels);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id) {
			var employee = _employeeRepository.GetById(id);
			if (employee == null) {
				return NotFound();
			}
			var employeeViewModel = new EmployeeViewModel {
				Name = employee.Name,
				Age = employee.Age.ToString()
			};
			return Ok(employeeViewModel);
		}

		[HttpDelete]
		public IActionResult Delete(int id) {
			var employees = _employeeRepository.GetAll();
			var employee = employees.FirstOrDefault(e => e.Id == id);
			if (employee == null) {
				return NotFound();
			}
			_employeeRepository.Delete(employee);
			return Ok();
		}

	}
}
