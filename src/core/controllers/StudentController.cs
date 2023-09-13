using DB;

class StudentController
{
    private NotenDB database;
    private AccessManager accessManager;
    public StudentController(NotenDB db, AccessManager access)
    {
        database = db;
        accessManager = access;
    }
    public List<Student> GetAll(UserSessionAuth auth)
    {
        accessManager.CheckAdminAccess(auth);
        return database.students.GetAll();
    }
    public Student GetById(UserSessionAuth auth, int id)
    {
        accessManager.CheckSpecificStudentOrTeacherOrAdminAccess(auth, auth.studentId);
        return database.students.GetById(id);
    }

    public List<Student> GetWhereName(UserSessionAuth auth, string name)
    {
        accessManager.CheckAdminAccess(auth);
        return database.students.GetWhereName(name);
    }
    public List<Student> GetWhereClass(UserSessionAuth auth, int classId)
    {
        accessManager.CheckTeacherOrAdminAccess(auth);
        return database.students.GetWhereClass(classId);
    }
    public int Create(UserSessionAuth auth, string loginId, string password, string name)
    {
        accessManager.CheckAdminAccess(auth);
        int userId;
        if (loginId == auth.loginId && auth.studentId is null)
        {
            userId = database.users.GetIdByLoginId(loginId);
        }
        else
        {
            userId = database.users.Create(loginId, password, false);
        }
        int studentId = database.students.Create(userId, name);
        return studentId;
    }
}