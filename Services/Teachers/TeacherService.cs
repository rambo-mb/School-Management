using SM.Models.Teachers;
using SM.Repositories.Teachers;
using SM.Services.Common;

namespace SM.Services.Teachers;

public class TeacherService : BaseService<Teacher>, ITeacherService 
{
	public TeacherService(ITeacherRepository teacherRepository) : base(teacherRepository)
	{
		
	}

	public List<Teacher> GetTeachersByName(string name) =>
		GetAll().Where(item => item.FirstName.ToLower().Contains(name.ToLower()) || item.LastName.ToLower().Contains(name.ToLower())).ToList();

}