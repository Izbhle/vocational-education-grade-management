using DB;

class CourseController
{
    private NotenDB database;
    private AccessManager accessManager;
    public CourseController(NotenDB db, AccessManager access)
    {
        database = db;
        accessManager = access;
    }
    public Course GetById(UserSessionAuth auth, int id)
    {
        accessManager.CheckAdminAccess(auth);
        return database.courses.GetById(id);
    }

    public List<Course> GetWhereName(UserSessionAuth auth, int classId)
    {
        accessManager.CheckAdminAccess(auth);
        return database.courses.GetWhereClass(classId);
    }
    public List<Course> GetWhereClass(UserSessionAuth auth, int classId)
    {
        accessManager.CheckTeacherOrAdminOrClassMemberAccess(auth, classId);
        return database.courses.GetWhereClass(classId);
    }
    public List<Course> GetWhereClassAndTeacher(UserSessionAuth auth, int classId, int teacherId)
    {
        accessManager.CheckTeacherAccess(auth);
        return database.courses.GetWhereClassAndTeacher(classId, teacherId);
    }
    public int Create(UserSessionAuth auth, string name, int classId, int teaherId, string semester)
    {
        accessManager.CheckAdminAccess(auth);
        int courseId = database.courses.Create(name, classId, teaherId, semester);
        return courseId;
    }
}