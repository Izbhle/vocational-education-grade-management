using DB;

class GradeController
{
    private NotenDB database;
    private AccessManager accessManager;
    public GradeController(NotenDB db, AccessManager access)
    {
        database = db;
        accessManager = access;
    }
    public List<Grade> GetWhereStudentAndCourse(UserSessionAuth auth, int studentId, int courseId)
    {
        accessManager.CheckSpecificStudentOrCourseTeacherOrAdminAccess(auth, courseId, studentId);
        return database.grades.GetWhereStudentAndCourse(studentId, courseId);
    }
    public int Create(
        UserSessionAuth auth,             
        int studentId,
        int courseId,
        string grade,
        string date,
        int noteTypeId)
    {
        accessManager.CheckCourseTeacherOrAdminAccess(auth, courseId);
        int gradeId = database.grades.Create(studentId, courseId, grade, date, noteTypeId);
        return gradeId;
    }
    public void Delete(
        UserSessionAuth auth,             
        int gradeId)
    {
        Grade grade = database.grades.GetById(gradeId);
        accessManager.CheckCourseTeacherOrAdminAccess(auth, grade.KursId);
        database.grades.Delete(gradeId);
        return;
    }
}