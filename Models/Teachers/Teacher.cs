using SM.Models.Common;

namespace SM.Models.Teachers;

public class Teacher : IModel
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Subject { get; set; }

}