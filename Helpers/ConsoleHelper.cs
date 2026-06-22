using SM.Models.Students;
using SM.Models.Teachers;

namespace SM.Helpers;

public static class ConsoleHelper
{
	public static void PrintError(string message)
	{
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Red;
		Log.Write(message);
		Console.ResetColor();

	}

	public static void PrintWarning(string message)
	{
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Yellow;
		Log.Write(message);
		Console.ResetColor();
	}

	public static void PrintSuccess(string message)
	{
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Green;
		Log.Write(message);
		Console.ResetColor();
	}

	public static void PrintContinue()
	{
		Console.WriteLine("Press any key to continue...");
		Console.ReadKey();
	}

	public static void PrintTeacherInfo(Teacher teacher)
	{
		Console.WriteLine(
			$"""
			Teacher {teacher.Id} Info:
			==========================
			First name: {teacher.FirstName},
			Last name: {teacher.LastName},
			Subject: {teacher.Subject}

			"""
		);
	}

	public static void PrintStudentInfo(Student student)
	{
		Console.WriteLine(
			$"""
			Student {student.Id} Info:
			==========================
			First name: {student.FirstName},
			Last name: {student.LastName},
			Class number: {student.Class}
			
			"""
		);
	}

	public static int ValidateInt(string name)
	{
		int result;
		bool isValid = true;

		do
		{
			Console.Write($"Enter {name}: ");
			string userInput = Console.ReadLine().Trim();

			if (userInput == "0" || userInput.ToLower() == "q") return 0;

			isValid = int.TryParse(userInput, out result) && result > 0;

			if (!isValid) PrintError($"Field {name} is not valid");

		} while (!isValid);

		return result;
	}

	public static string ValidateString(string name)
	{
		string result = string.Empty;
		bool isValid = true;

		do
		{
			Console.Write($"Enter {name}: ");
			string userInput = Console.ReadLine().Trim();

			if (userInput == "0" || userInput.ToLower() == "q") return null;

			if (string.IsNullOrWhiteSpace(userInput))
			{
				isValid = false;
				PrintError($"Field {name} is not valid");
			}
			else
			{
				result = userInput;
				isValid = true;
			}

		} while (!isValid);

		return result;
	}
}