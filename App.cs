using SM.Apps;
using SM.Helpers;

namespace SM;

public class App
{
    private readonly TeacherApp _teacherApp;
    private readonly StudentApp _studentApp;

    public App(TeacherApp teacherApp, StudentApp studentApp)
    {
        _teacherApp = teacherApp;
        _studentApp = studentApp;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== SCHOOL MANAGEMENT =====");
            Console.WriteLine("1. Teachers");
            Console.WriteLine("2. Students");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose an option: ");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    _teacherApp.Run();
                    break;
                case "2":
                    _studentApp.Run();
                    break;
                case "0": return;
                default:
                    ConsoleHelper.PrintError("Invalid option, try again");
                    ConsoleHelper.PrintContinue();
                    break;
            }
        }
    }
}