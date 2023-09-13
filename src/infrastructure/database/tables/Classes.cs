namespace DB 
{
    public class Classes
    {
        private Database db;
        public Class GetById(int id)
        {
            var response = db.Select(
                "Klasse",
                Utils.Collumns("Id", "Name"),
                Utils.WhereStatements(("Id", id, WhereChainType.And, WhereComparisonType.Equals))
            );
            return new Class(response[0]);
        }
        public List<Class> GetAll()
        {
            var response = db.Select(
                "Klasse",
                Utils.Collumns("Id", "Name")
            );
            return response.Select(row => new Class(row)).ToList();
        }
        public List<Class> GetWhereName(string name)
        {
            var response = db.Select(
                "Klasse",
                Utils.Collumns("Id", "Name"),
                Utils.WhereStatements(("Name", $"%{name}%", WhereChainType.And, WhereComparisonType.Like))
            );
            return response.Select(row => new Class(row)).ToList();
        }
        public List<Class> GetAllForStudent(int studentId)
        {
            var response = db.Select(
                "Klasse",
                Utils.Joins(("Einschreibung", "Klasse")),
                Utils.Collumns(("Klasse", "Id"), ("Klasse", "Name")),
                Utils.WhereStatements(("Einschreibung", "SchuelerId", studentId, WhereChainType.And, WhereComparisonType.Equals))
            );
            return response.Select(row => new Class(row)).ToList();
        }
        public List<Class> GetAllForTeacher(int teacherId)
        {
            var response = db.Select(
                "Klasse",
                Utils.Joins(("Kurs", "Klasse")),
                Utils.Collumns(("Klasse", "Id"), ("Klasse", "Name")),
                Utils.WhereStatements(("Kurs", "LehrerId", teacherId, WhereChainType.And, WhereComparisonType.Equals))
            );
            return response.Select(row => new Class(row)).ToList();
        }
        public int Create(string Name)
        {
            return db.Create(
                "Klasse",
                Utils.Collumns("Name"),
                Utils.Values(Name)
            );
        }
        public Classes(Database initializedDb)
        {
            db = initializedDb;
        }
    }
}