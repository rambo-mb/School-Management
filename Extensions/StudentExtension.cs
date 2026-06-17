using SM.Models.Students;

namespace SM.Extensions;

public static class StudentExtension
{
    public static Student FindFirstOrDefaultCleverStudent(this List<Student> students)
    {
        return students.MaxBy(student => student.Grade);
    }

    public static Student FindFirstOrDefaultYoungestStudent(this List<Student> students)
    {
        return students.MinBy(student => student.Age);
    }
}