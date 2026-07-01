using SM.Exceptions;
using SM.Models.Students;
using SM.Repositories.Common;

namespace SM.Repositories.Students;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(string filePath) : base(filePath)
    {

    }

    public override async Task UpdateAsync(Student item)
    {
        Student updatedItem = GetById(item.Id);

        if (updatedItem is null)
            throw new NotFoundException($"Student with ID {item.Id} not found");

        updatedItem.FirstName = item.FirstName;
        updatedItem.LastName = item.LastName;
        updatedItem.Class = item.Class;

        await SaveItemsAsync();
    }

    protected override void ValidateItem(Student item)
    {
        if (string.IsNullOrWhiteSpace(item.FirstName))
            throw new ValidationException("First name cannot be null");
        if (string.IsNullOrWhiteSpace(item.LastName))
            throw new ValidationException("Last name cannot be null");
        if (string.IsNullOrWhiteSpace(item.Class))
            throw new ValidationException("Class cannot be null");
    }
}