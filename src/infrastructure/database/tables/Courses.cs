namespace DB 
{
    public class Courses
    {
        private Database db;
        public Course GetById(int id)
        {
            var response = db.Select(
                "Kurs",
                Utils.Collumns("Id", "Name", "KlasseId", "LehrerId", "Schuljahr"),
                Utils.WhereStatements(("Id", id, WhereChainType.And, WhereComparisonType.Equals))
            );
            return new Course(response[0]);
        }
        public List<Course> GetWhereClass(int classId)
        {
            var response = db.Select(
                "Kurs",
                Utils.Collumns("Id", "Name", "KlasseId", "LehrerId", "Schuljahr"),
                Utils.WhereStatements(("KlasseId", classId, WhereChainType.And, WhereComparisonType.Equals))
            );
            return response.Select(row => new Course(row)).ToList();
        }

        public List<Course> GetWhereClassAndTeacher(int classId, int teacherId)
        {
            var response = db.Select(
                "Kurs",
                Utils.Collumns("Id", "Name", "KlasseId", "LehrerId", "Schuljahr"),
                Utils.WhereStatements(
                    ("KlasseId", classId, WhereChainType.And, WhereComparisonType.Equals),
                    ("LehrerId", teacherId, WhereChainType.And, WhereComparisonType.Equals)
                )
            );
            return response.Select(row => new Course(row)).ToList();
        }
        public int Create(string Name, int KlasseId, int LehrerId, string Schuljahr)
        {
            return db.Create(
                "Kurs",
                Utils.Collumns("Name", "KlasseId", "LehrerId", "Schuljahr"),
                Utils.Values(Name, KlasseId, LehrerId, Schuljahr)
            );
        }
        public Courses(Database initializedDb)
        {
            db = initializedDb;
        }
    }
}