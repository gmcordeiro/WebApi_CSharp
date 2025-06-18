using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Model.Employee {

	[Table("employee")]
	public class Employee(string name, int age, string photo) {

		[Key]
		[Column("id")]
		public int Id { get; private set; }

		[Column("name")]
		public string Name { get; set; } = name ?? throw new ArgumentNullException(nameof(name));

		[Column("age")]
		public int Age { get; set; } = age;

		[Column("photo")]
		public string? Photo { get; set; } = photo;
	}

}
