using SM.Models.Teachers;
using SM.Repositories.Common;

namespace SM.Repositories.Teachers;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository 
{
	public TeacherRepository(string filePath) : base(filePath)
	{
		
	}

	public override void Update(Teacher item)
	{
		Teacher updatedItem = GetById(item.Id);

		updatedItem.FirstName = item.FirstName;
		updatedItem.LastName = item.LastName;
		updatedItem.Subject = item.Subject;

		SaveItems();
	}
}