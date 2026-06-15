using SM.Models.Students;
using SM.Repositories.Common;

namespace SM.Repositories.Students;

public class StudentRepository : BaseRepository<Student>, IStudentRepository 
{
	public StudentRepository(string filePath) : base(filePath)
	{
		
	}

	public override void Update(Student item)
	{
		Student updatedItem = GetById(item.Id);

		updatedItem.FirstName = item.FirstName;
		updatedItem.LastName = item.LastName;
		updatedItem.Class = item.Class;

		SaveItems();
	}
}