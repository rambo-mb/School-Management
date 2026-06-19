using SM;
using SM.Apps;
using SM.Helpers;
using SM.Repositories.Students;
using SM.Repositories.Teachers;
using SM.Services.Students;
using SM.Services.Teachers;

ITeacherRepository teacherRepository = new TeacherRepository("teachers.json");
IStudentRepository studentRepository = new StudentRepository("students.json");
TeacherService teacherService = new TeacherService(teacherRepository);
StudentService studentService = new StudentService(studentRepository);
TeacherApp teacherApp = new TeacherApp(teacherService);
StudentApp studentApp = new StudentApp(studentService);
App app = new App(teacherApp, studentApp);
Log.Write("Dastur ishga tushdi");
app.Run();