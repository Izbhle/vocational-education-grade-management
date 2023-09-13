using DB;


class OperationsController
{
    private NotenDB database;
    private AccessManager accessManager;
    public StudentController students;
    public TeacherController teachers;
    public ClassController classes;
    public CourseController courses;
    public GradeController grades;
    public OperationsController(NotenDB db)
    {
        database = db;
        accessManager = new AccessManager(db);
        students = new StudentController(database, accessManager);
        teachers = new TeacherController(database, accessManager);
        classes = new ClassController(database, accessManager);
        courses = new CourseController(database, accessManager);
        grades = new GradeController(database, accessManager);
    }
}