using SM.Models.Teachers;
using SM.Services.Common;

namespace SM.Services.Teachers;

public interface ITeacherService : IGenericService<Teacher>
{
	List<Teacher> GetTeachersByName(string name);
}