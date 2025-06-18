using Microsoft.AspNetCore.Mvc;
using WebApi.Model.Employee;
using WebApi.ViewModel;

namespace WebApi.Controllers {

	[ApiController]
	[Route("api/v1/employee")]
	public class EmployeeController(IEmployeeRepository employeeRepository) : Controller {

		private readonly IEmployeeRepository _employeeRepository = employeeRepository;

		[HttpPost]
		public IActionResult Add([FromForm] EmployeeCommand employeeView) {

			string filePath = GetFilePath(employeeView);

			var employee = new Employee(
				name: employeeView.Name,
				age: int.Parse(employeeView.Age),
				photo: filePath
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

			var filePath = GetFilePath(employeeCommand);
			if (string.IsNullOrEmpty(filePath)) {
				filePath = employee.Photo;
			}

			employee.Age = int.Parse(employeeCommand.Age);
			employee.Name = employeeCommand.Name;
			employee.Photo = filePath;

			employee = _employeeRepository.Update(employee);

			return Ok(employee);

		}

		[HttpGet]
		public IActionResult GetAll() {

			var employees = _employeeRepository.GetAll();


			var employeeViewModels = employees.Select(e => new EmployeeViewModel {
				Id = e.Id,
				Name = e.Name,
				Age = e.Age.ToString(),
				Photo = GetFile(e.Photo)
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
				Age = employee.Age.ToString(),
				Photo = GetFile(employee.Photo)
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

		private static string GetFilePath(EmployeeCommand employeeCommand) {
			var filePath = string.Empty;

			var hasPhoto = employeeCommand.Photo != null && employeeCommand.Photo.Length > 0;

			if (hasPhoto) {
				filePath = Path.Combine("Storage", employeeCommand.Photo.FileName);
				using Stream fileStream = new FileStream(filePath, FileMode.Create);
				employeeCommand.Photo.CopyTo(fileStream);
			}

			return filePath;
		}

		private static FormFile? GetFile(string filePath) {

			if (string.IsNullOrEmpty(filePath)) {
				return null;
			}

			var file = new FormFile(new MemoryStream(), 0, 0, "file", Path.GetFileName(filePath)) {
				Headers = new HeaderDictionary(),
				ContentType = "application/octet-stream"
			};
			return file;
		}

	}
}
