using SM.Helpers;
using SM.Models.Students;
using SM.Services.Students;

namespace SM.Apps;

public class StudentApp
{
    private readonly IStudentService _service;

    public StudentApp(IStudentService studentService)
    {
        _service = studentService;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== STUDENTS MENU =====");
            Console.WriteLine("1. Show students");
            Console.WriteLine("2. Create student");
            Console.WriteLine("3. Show student by ID");
            Console.WriteLine("4. Update student by ID");
            Console.WriteLine("5. Delete student by ID");
            Console.WriteLine("6. Print classes");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose an option: ");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    HandleReadAll();
                    break;
                case "2":
                    HandleCreate();
                    break;
                case "3":
                    HandleReadById();
                    break;
                case "4":
                    HandleUpdateById();
                    break;
                case "5":
                    HandleDeleteById();
                    break;
                case "6":
                    HandleGetClasses();
                    break;
                case "0": return;
                default:
                    ConsoleHelper.PrintError("Invalid option, try again");
                    ConsoleHelper.PrintContinue();
                    break;
            }
        }
    }

    private void HandleGetClasses()
    {
        Console.Clear();
        Console.WriteLine("===== CLASSES =====\n");

        // _service.GetStudentsByClass();

        ConsoleHelper.PrintContinue();
    }

    private void HandleDeleteById()
    {
        Console.Clear();
        Console.WriteLine("===== DELETE STUDENT =====\n");

        int studentId = ConsoleHelper.ValidateInt("student ID");
        if (studentId == 0) return;

        Student student = _service.GetById(studentId);

        if (student is null)
        {
            ConsoleHelper.PrintWarning("Student with this ID is not found");
        }
        else
        {
            _service.Delete(studentId);
            ConsoleHelper.PrintSuccess("Student deleted successfully");
        }

        ConsoleHelper.PrintContinue();
    }

    private void HandleUpdateById()
    {
        Console.Clear();
        Console.WriteLine("===== UPDATE STUDENT =====\n");

        int studentId = ConsoleHelper.ValidateInt("student ID");
        if (studentId == 0) return;

        Student student = _service.GetById(studentId);

        if (student is null)
        {
            ConsoleHelper.PrintWarning("Student with this ID is not found");
        }
        else
        {
            string studentFirstName = ConsoleHelper.ValidateString("first name");
            if (studentFirstName is null) return;

            string studentLastName = ConsoleHelper.ValidateString("last name");
            if (studentLastName is null) return;

            string studentClassName = ConsoleHelper.ValidateString("student's class");
            if (studentClassName is null) return;

            Student newStudent = new Student()
            {
                Id = student.Id,
                FirstName = studentFirstName,
                LastName = studentLastName,
                Class = studentClassName
            };

            _service.Update(newStudent);
            ConsoleHelper.PrintSuccess("Student updated successfully");
        }

        ConsoleHelper.PrintContinue();
    }

    private void HandleReadById()
    {
        Console.Clear();
        Console.WriteLine("===== FIND STUDENT =====\n");

        int studentId = ConsoleHelper.ValidateInt("student ID");
        if (studentId == 0) return;

        Student student = _service.GetById(studentId);

        if (student is null)
            ConsoleHelper.PrintWarning("Student with this ID not found");
        else
        {
            Console.WriteLine();
            ConsoleHelper.PrintStudentInfo(student);
        }

        ConsoleHelper.PrintContinue();
    }

    private void HandleReadAll()
    {
        Console.Clear();
        Console.WriteLine("===== ALL STUDENTS =====");

        List<Student> students = _service.GetAll();

        if (students.Count == 0)
        {
            ConsoleHelper.PrintWarning("Students not found");
        }
        else
        {
            Console.WriteLine();
            foreach (Student student in students)
            {
                ConsoleHelper.PrintStudentInfo(student);
            }
        }

        ConsoleHelper.PrintContinue();
    }

    private void HandleCreate()
    {
        Console.Clear();
        Console.WriteLine("===== CREATE STUDENT =====\n");

        string studentFirstName = ConsoleHelper.ValidateString("first name");
        if (studentFirstName is null) return;

        string studentLastName = ConsoleHelper.ValidateString("last name");
        if (studentLastName is null) return;

        string studentClassName = ConsoleHelper.ValidateString("student's class");
        if (studentClassName is null) return;

        Student newStudent = new Student()
        {
            FirstName = studentFirstName,
            LastName = studentLastName,
            Class = studentClassName
        };

        _service.Add(newStudent);

        ConsoleHelper.PrintSuccess("Student created successfully");
        ConsoleHelper.PrintContinue();
    }
}