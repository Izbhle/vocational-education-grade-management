namespace DB 
{
    public class NotenDB
    {
        private Database db;
        public Users users;
        public Students students;
        public Teachers teachers;
        public Classes classes;
        public Courses courses;
        public Grades grades;
        public Enrollments enrollments;
        public NotenDB(string dbFilePath, string migrationsPath)
        {
            db = new SQLiteDB(dbFilePath, migrationsPath);
            users = new Users(db);
            students = new Students(db);
            teachers = new Teachers(db);
            classes = new Classes(db);
            courses = new Courses(db);
            grades = new Grades(db);
            enrollments = new Enrollments(db);
        }
    }
}