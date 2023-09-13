using DB;

class TeacherController
{
    private NotenDB database;
    private AccessManager accessManager;
    public TeacherController(NotenDB db, AccessManager access)
    {
        database = db;
        accessManager = access;
    }
    public List<Teacher> GetAll(UserSessionAuth auth)
    {
        accessManager.CheckAdminAccess(auth);
        return database.teachers.GetAll();
    }
    public Teacher GetById(UserSessionAuth auth, int id)
    {
        accessManager.CheckSpecificTeacherOrAdminAccess(auth, id);
        return database.teachers.GetById(id);
    }

    public List<Teacher> GetWhereName(UserSessionAuth auth, string name)
    {
        accessManager.CheckAdminAccess(auth);
        return database.teachers.GetWhereName(name);
    }
    public int Create(UserSessionAuth auth, string loginId, string password, string name)
    {
        accessManager.CheckAdminAccess(auth);
        int userId;
        if (loginId == auth.loginId && auth.teacherId is null)
        {
            userId = database.users.GetIdByLoginId(loginId);
        }
        else
        {
            userId = database.users.Create(loginId, password, false);
        }
        int teacherId = database.teachers.Create(userId, name);
        return teacherId;
    }
}