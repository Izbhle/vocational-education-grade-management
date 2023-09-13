using DB;

class ClassController
{
    private NotenDB database;
    private AccessManager accessManager;
    public ClassController(NotenDB db, AccessManager access)
    {
        database = db;
        accessManager = access;
    }
    public List<Class> GetAll(UserSessionAuth auth)
    {
        accessManager.CheckAdminAccess(auth);
        return database.classes.GetAll();
    }
    public Class GetById(UserSessionAuth auth, int id)
    {
        accessManager.CheckAdminAccess(auth);
        return database.classes.GetById(id);
    }

    public List<Class> GetWhereName(UserSessionAuth auth, string name)
    {
        accessManager.CheckAdminAccess(auth);
        return database.classes.GetWhereName(name);
    }
    public List<Class> GetAllForStudent(UserSessionAuth auth, int studentId)
    {
        accessManager.CheckSpecificStudentOrAdminAccess(auth, studentId);
        return database.classes.GetAllForStudent(studentId);
    }
    public List<Class> GetAllForTeacher(UserSessionAuth auth, int teacherId)
    {
        accessManager.CheckTeacherOrAdminAccess(auth);
        return database.classes.GetAllForTeacher(teacherId);
    }
    public int Create(UserSessionAuth auth, string name)
    {
        accessManager.CheckAdminAccess(auth);
        int classId = database.classes.Create(name);
        return classId;
    }
    public int AddStudentToClass(UserSessionAuth auth, int studentId, int classId)
    {
        accessManager.CheckAdminAccess(auth);
        return database.enrollments.Create(studentId, classId);
    }}