using SM.Models.Common;

namespace SM.Models.Students;

public class Student : IModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Class { get; set; }
    public int Age { get; set; }
    public int Grade { get; set; }
}