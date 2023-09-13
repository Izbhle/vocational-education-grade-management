class Seed
{
    private UserSessionAuth auth;
    private OperationsController operations;
    private Random random;
    public Seed(UserSessionAuth au, OperationsController ops)
    {
        auth = au;
        operations = ops;
        random = new Random();
    }
    public void Run()
    {
        string[] studentNames = {"Timo", "Max", "Sina", "Petra", "Patrick", "Simon", "Hannah", "Lara"};
        var studentId = new Dictionary<string, int>();
        foreach (string name in studentNames)
        {
            studentId[name] = operations.students.Create(auth, name, "", name);
        }
        string[] teacherNames = {"Stefan", "Stefen", "Steven", "Steffani", "Steffi"};
        var teacherId = new Dictionary<string, int>();
        foreach (string name in teacherNames)
        {
            teacherId[name] = operations.teachers.Create(auth, name, "", name);
        }
        string[] classNames = {"IA001", "IA002"};
        string[] courseNames = {"SDM", "EVP", "WBL"};
        string[] schoolYears = {"01-2023", "02-2023"};
        foreach (string name in classNames)
        {
            int classId = operations.classes.Create(auth, name);
            // Assign Students to classes
            operations.classes.AddStudentToClass(auth, (classId - 1) * 4 + 1, classId);
            operations.classes.AddStudentToClass(auth, (classId - 1) * 4 + 2, classId);
            operations.classes.AddStudentToClass(auth, (classId - 1) * 4 + 3, classId);
            operations.classes.AddStudentToClass(auth, (classId - 1) * 4 + 4, classId);
            // Create Courses
            foreach (string courseName in courseNames)
            {
                foreach (string schoolYear in schoolYears)
                {
                    int courseId = operations.courses.Create(auth, courseName, classId, random.Next(1,6), schoolYear);
                    for (int i=0; i<4; i++)
                    {
                        int rndYear = random.Next(2022, 2024);
                        int rndMonth = random.Next(1, 13);
                        int rndDay = random.Next(1, 32);
                        int noteType = random.Next(1, 3);

                        operations.grades.Create(auth, (classId - 1) * 4 + 1, courseId, random.Next(1,7).ToString(), $"{rndDay}.{rndMonth}.{rndYear}", noteType);
                        operations.grades.Create(auth, (classId - 1) * 4 + 2, courseId, random.Next(1,7).ToString(), $"{rndDay}.{rndMonth}.{rndYear}", noteType);
                        operations.grades.Create(auth, (classId - 1) * 4 + 3, courseId, random.Next(1,7).ToString(), $"{rndDay}.{rndMonth}.{rndYear}", noteType);
                        operations.grades.Create(auth, (classId - 1) * 4 + 4, courseId, random.Next(1,7).ToString(), $"{rndDay}.{rndMonth}.{rndYear}", noteType);
                    }
                }
            }
        }

    }
}