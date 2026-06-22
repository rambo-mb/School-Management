using SM.Models.Students;
using SM.Repositories.Students;
using SM.Services.Common;
using SM.Services.Students;

namespace SM.Services.Students;

public class StudentService : BaseService<Student>, IStudentService
{
    public StudentService(IStudentRepository studentRepository) : base(studentRepository)
    {

    }

    public Dictionary<string, int> GetStudentByClass()
    {
        return GetAll()
            .GroupBy(student => student.Class)
            .OrderBy(group => group.Key)
            .ToDictionary(group => group.Key, group => group.Count());
    }
}