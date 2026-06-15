using SM.Services.Students;

namespace SM.Apps;

public class StudentApp
{
	private IStudentService studentService;

	public StudentApp(IStudentService studentService)
	{
		this.studentService = studentService;
	}
}