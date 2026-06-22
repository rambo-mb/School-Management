using SM.Exceptions;
using SM.Helpers;
using SM.Models.Teachers;
using SM.Services.Teachers;

namespace SM.Apps;

public class TeacherApp
{
    private readonly ITeacherService _service;

    public TeacherApp(ITeacherService teacherService)
    {
        _service = teacherService;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== TEACHERS MENU =====");
            Console.WriteLine("1. Show teachers");
            Console.WriteLine("2. Create teacher");
            Console.WriteLine("3. Show teacher by ID");
            Console.WriteLine("4. Update teacher by ID");
            Console.WriteLine("5. Delete teacher by ID");
            Console.WriteLine("6. Show teachers with pagination");
            Console.WriteLine("7. Search teachers with name");
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
                    HandleShowWithPagination();
                    break;
                case "7":
                    HandleSearchTeachersByName();
                    break;
                case "0": return;
                default:
                    ConsoleHelper.PrintError("Invalid option, try again");
                    ConsoleHelper.PrintContinue();
                    break;
            }
        }
    }

    private void HandleSearchTeachersByName()
    {
        Console.Clear();
        Console.WriteLine("===== SEARCH TEACHER =====\n");

        string name = ConsoleHelper.ValidateString("name");
        if (name is null) return;

        List<Teacher> teachers = _service.GetTeachersByName(name);

        if (teachers.Count == 0)
        {
            ConsoleHelper.PrintWarning("Teachers not found");
        }
        else
        {
            Console.WriteLine();
            foreach (Teacher teacher in teachers)
            {
                ConsoleHelper.PrintTeacherInfo(teacher);
            }
        }

        ConsoleHelper.PrintContinue();
    }

    private void HandleShowWithPagination()
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
                ConsoleHelper.PrintWarning("Teachers not found");
                ConsoleHelper.PrintContinue();
                return;
            }

            Console.WriteLine($"===== TEACHERS - {(currentPage - 1) * pageSize + 1}-{Math.Min(currentPage * pageSize, (int)totalItems)} =====");

            List<Teacher> teachers = _service.GetPaginatedItems(currentPage, pageSize);

            Console.WriteLine();
            foreach (Teacher teacher in teachers)
            {
                ConsoleHelper.PrintTeacherInfo(teacher);
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

    private void HandleDeleteById()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("===== DELETE TEACHER =====\n");

            int teacherId = ConsoleHelper.ValidateInt("teacher ID");
            if (teacherId == 0) return;

            _service.Delete(teacherId);
            ConsoleHelper.PrintSuccess("Teacher deleted successfully");

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

    private void HandleUpdateById()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE TEACHER =====\n");

            int teacherId = ConsoleHelper.ValidateInt("teacher ID");
            if (teacherId == 0) return;

            Teacher teacher = _service.GetById(teacherId);

            string teacherFirstName = ConsoleHelper.ValidateString("first name");
            if (teacherFirstName is null) return;

            string teacherLastName = ConsoleHelper.ValidateString("last name");
            if (teacherLastName is null) return;

            string teacherSubject = ConsoleHelper.ValidateString("teacher's subject");
            if (teacherSubject is null) return;

            Teacher newTeacher = new Teacher()
            {
                Id = teacher.Id,
                FirstName = teacherFirstName,
                LastName = teacherLastName,
                Subject = teacherSubject
            };

            _service.Update(newTeacher);
            ConsoleHelper.PrintSuccess("Teacher updated successfully");

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
            Console.WriteLine("===== FIND TEACHER =====\n");

            int teacherId = ConsoleHelper.ValidateInt("teacher ID");
            if (teacherId == 0) return;

            Teacher teacher = _service.GetById(teacherId);

            Console.WriteLine();
            ConsoleHelper.PrintTeacherInfo(teacher);

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

    private void HandleCreate()
    {
        try
        {
            Console.Clear();
            Console.WriteLine("===== CREATE TEACHER =====\n");

            string firstName = ConsoleHelper.ValidateString("teacher's first name");
            if (firstName is null) return;

            string lastName = ConsoleHelper.ValidateString("teacher's last name");
            if (lastName is null) return;

            string subject = ConsoleHelper.ValidateString("teacher's subject");
            if (subject is null) return;

            Teacher newTeacher = new Teacher()
            {
                FirstName = firstName,
                LastName = lastName,
                Subject = subject
            };

            _service.Add(newTeacher);

            ConsoleHelper.PrintSuccess("Teacher created successfully");
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

    private void HandleReadAll()
    {
        Console.Clear();
        Console.WriteLine("===== ALL TEACHERS =====");

        List<Teacher> teachers = _service.GetAll();

        if (teachers.Count == 0)
            ConsoleHelper.PrintWarning("Teachers not found!");
        else
        {
            Console.WriteLine();
            foreach (Teacher teacher in teachers)
                ConsoleHelper.PrintTeacherInfo(teacher);
        }

        ConsoleHelper.PrintContinue();
    }
}