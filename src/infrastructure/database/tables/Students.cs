namespace DB 
{
    public class Students
    {
        private Database db;
        public Student GetById(int id)
        {
            var response = db.Select(
                "Schueler",
                Utils.Collumns("Id", "Name"),
                Utils.WhereStatements(("Id", id, WhereChainType.And, WhereComparisonType.Equals))
            );
            return new Student(response[0]);
        }
        public List<Student> GetAll()
        {
            var response = db.Select(
                "Schueler",
                Utils.Collumns("Id", "Name")
            );
            return response.Select(row => new Student(row)).ToList();
        }
        public List<Student> GetWhereName(string name)
        {
            var response = db.Select(
                "Schueler",
                Utils.Collumns("Id", "Name"),
                Utils.WhereStatements(("Name", $"%{name}%", WhereChainType.And, WhereComparisonType.Like))
            );
            return response.Select(row => new Student(row)).ToList();
        }
        public List<Student> GetWhereClass(int classId)
        {
            var response = db.Select(
                "Schueler",
                Utils.Joins(("Einschreibung", "Schueler")),
                Utils.Collumns(("Schueler", "Id"), ("Schueler", "Name")),
                Utils.WhereStatements(("Einschreibung", "KlasseId", classId, WhereChainType.And, WhereComparisonType.Equals))
            );
            return response.Select(row => new Student(row)).ToList();
        }
        public int Create(int BenutzerID, string Name)
        {
            return db.Create(
                "Schueler",
                Utils.Collumns("BenutzerId", "Name"),
                Utils.Values(BenutzerID, Name)
            );
        }
        public Students(Database initializedDb)
        {
            db = initializedDb;
        }
    }
}