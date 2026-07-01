using SM.Exceptions;
using SM.Extensions.Common;
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

    public async Task RunAsync()
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
            Console.WriteLine("7. Show students with pagination");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose an option: ");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    HandleReadAll();
                    break;
                case "2":
                    await HandleCreateAsync();
                    break;
                case "3":
                    HandleReadById();
                    break;
                case "4":
                    await HandleUpdateByIdAsync();
                    break;
                case "5":
                    await HandleDeleteByIdAsync();
                    break;
                case "6":
                    HandleGetClasses();
                    break;
                case "7":
                    HandleShowStudentsWithPagination();
                    break;
                case "0": return;
                default:
                    ConsoleHelper.PrintError("Invalid option, try again");
                    ConsoleHelper.PrintContinue();
                    break;
            }
        }
    }

    private void HandleShowStudentsWithPagination()
    {
        int currentPage = 1;
        int pageSize = 5;

        while (true)
        {
            Console.Clear();
            int totalItems = _service.GetCount();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);


            if (totalItems == 0)
            {
                ConsoleHelper.PrintWarning("Students not found");
                ConsoleHelper.PrintContinue();
                return;
            }

            Console.WriteLine($"===== STUDENTS - {(currentPage - 1) * pageSize + 1}-{Math.Min(currentPage * pageSize, (int)totalItems)} =====");

            List<Student> students = _service.GetAll().Paginate(currentPage, pageSize).ToList();

            Console.WriteLine();
            foreach (Student student in students)
            {
                ConsoleHelper.PrintStudentInfo(student);
            }

            if (currentPage != 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("< ");
                Console.ResetColor();
            }

            for (int i = 1; i <= totalPages; i++)
            {
                if (i == currentPage)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($" {i} ");
                    Console.ResetColor();
                }
                else
                    Console.Write($" {i} ");
            }

            if (currentPage != totalPages)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(">");
                Console.ResetColor();
            }

            Console.Write("Press 'q' to quit...");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.RightArrow && currentPage < totalPages)
                currentPage++;
            else if (keyInfo.Key == ConsoleKey.LeftArrow && currentPage > 1)
                currentPage--;
            else if (keyInfo.Key == ConsoleKey.Q)
                return;
        }
    }

    private void HandleGetClasses()
    {
        Console.Clear();
        Console.WriteLine("===== CLASSES =====\n");

        Dictionary<string, int> classes = _service.GetStudentByClass();

        if (classes.Count == 0)
        {
            ConsoleHelper.PrintWarning("No students found");
            ConsoleHelper.PrintContinue();
            return;
        }

        foreach (var cls in classes)
        {
            Console.WriteLine($"Class: {cls.Key} - Students: {cls.Value}");
        }

        ConsoleHelper.PrintContinue();
    }

    private async Task HandleDeleteByIdAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("===== DELETE STUDENT =====\n");

            int studentId = ConsoleHelper.ValidateInt("student ID");
            if (studentId == 0) return;

            await _service.DeleteAsync(studentId);
            ConsoleHelper.PrintSuccess("Student deleted successfully");

            ConsoleHelper.PrintContinue();
        }
        catch (NotFoundException ex)
        {
            ConsoleHelper.PrintError(ex.Message);
            ConsoleHelper.PrintContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Error: {ex.Message}");
            ConsoleHelper.PrintContinue();
        }
    }

    private async Task HandleUpdateByIdAsync()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE STUDENT =====\n");

            int studentId = ConsoleHelper.ValidateInt("student ID");
            if (studentId == 0) return;

            Student student = _service.GetById(studentId);

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

            await _service.UpdateAsync(newStudent);
            ConsoleHelper.PrintSuccess("Student updated successfully");

            ConsoleHelper.PrintContinue();
        }
        catch (NotFoundException ex)
        {
            ConsoleHelper.PrintError(ex.Message);
            ConsoleHelper.PrintContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Error: {ex.Message}");
            ConsoleHelper.PrintContinue();
        }
    }

    private void HandleReadById()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("===== FIND STUDENT =====\n");

            int studentId = ConsoleHelper.ValidateInt("student ID");
            if (studentId == 0) return;

            Student student = _service.GetById(studentId);

            Console.WriteLine();
            ConsoleHelper.PrintStudentInfo(student);

            ConsoleHelper.PrintContinue();
        }
        catch (NotFoundException ex)
        {
            ConsoleHelper.PrintError(ex.Message);
            ConsoleHelper.PrintContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Error: {ex.Message}");
            ConsoleHelper.PrintContinue();
        }
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

    private async Task HandleCreateAsync()
    {
        try
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

            await _service.AddAsync(newStudent);

            ConsoleHelper.PrintSuccess("Student created successfully");
            ConsoleHelper.PrintContinue();
        }
        catch (ValidationException ex)
        {
            ConsoleHelper.PrintError(ex.Message);
            ConsoleHelper.PrintContinue();
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError($"Error: {ex.Message}");
            ConsoleHelper.PrintContinue();
        }
    }
}