using SM.Models.Students;
using SM.Services.Common;

namespace SM.Services.Students;

public interface IStudentService : IGenericService<Student>
{
    Dictionary<string, int> GetStudentByClass();
}