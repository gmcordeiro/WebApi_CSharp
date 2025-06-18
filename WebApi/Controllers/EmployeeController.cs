using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Employee;
using WebApi.ViewModel;

namespace WebApi.Controllers {

	[ApiController]
	[Route("api/v1/employee")]
	public class EmployeeController(IEmployeeRepository employeeRepository) : Controller {

		private readonly IEmployeeRepository _employeeRepository = employeeRepository;

		[HttpPost]
		public IActionResult Add(EmployeeCommand employeeView) {

			var employee = new Employee(
				name: employeeView.Name,
				age: int.Parse(employeeView.Age),
				photo: employeeView.Photo
			);

			_employeeRepository.Add(employee);

			return Ok();

		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, EmployeeCommand employeeCommand) {

			var employee = _employeeRepository.GetById(id);
			if (employee == null) {
				return NotFound();
			}

			employee.Age = int.Parse(employeeCommand.Age);
			employee.Name = employeeCommand.Name;
			employee.Photo = employeeCommand.Photo;

			employee = _employeeRepository.Update(employee);

			return Ok(employee);

		}

		[HttpGet]
		public IActionResult GetAll() {

			var employees = _employeeRepository.GetAll();

			var employeeViewModels = employees.Select(e => new EmployeeViewModel {
				Id = e.Id,
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
				Id = employee.Id,
				Name = employee.Name,
				Age = employee.Age.ToString()
			};

			return Ok(employeeViewModel);

		}

		[HttpDelete]
		public IActionResult Delete(int id) {

			var employee = _employeeRepository.GetById(id);

			if (employee == null) {
				return NotFound();
			}

			_employeeRepository.Delete(employee);

			return Ok();

		}

	}
}
