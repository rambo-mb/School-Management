using SM.Exceptions;
using SM.Models.Teachers;
using SM.Repositories.Common;

namespace SM.Repositories.Teachers;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(string filePath) : base(filePath)
    {

    }

    public override async Task UpdateAsync(Teacher item)
    {
        Teacher updatedItem = GetById(item.Id);

        if (updatedItem is null)
            throw new NotFoundException($"Teacher with ID {item.Id} not found");

        updatedItem.FirstName = item.FirstName;
        updatedItem.LastName = item.LastName;
        updatedItem.Subject = item.Subject;

        await SaveItemsAsync();
    }

    protected override void ValidateItem(Teacher item)
    {
        if (string.IsNullOrWhiteSpace(item.FirstName))
            throw new ValidationException("First name cannot be null");
        if (string.IsNullOrWhiteSpace(item.LastName))
            throw new ValidationException("Last name cannot be null");
        if (string.IsNullOrWhiteSpace(item.Subject))
            throw new ValidationException("Subject name cannot be null");
    }
}