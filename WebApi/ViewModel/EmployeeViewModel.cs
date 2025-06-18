namespace WebApi.ViewModel {

	public class EmployeeCommand {

		public required string Name { get; set; }
		public required string Age { get; set; }
		public IFormFile? Photo { get; set; }

	}

	public class EmployeeViewModel : EmployeeCommand {
		public required int Id { get; set; }
	}

}
