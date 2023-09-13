namespace DB 
{
    public class Teachers
    {
        private Database db;
        public Teacher GetById(int id)
        {
            var response = db.Select(
                "Lehrer",
                Utils.Collumns("Id", "Name"),
                Utils.WhereStatements(("Id", id, WhereChainType.And, WhereComparisonType.Equals))
            );
            return new Teacher(response[0]);
        }
        public List<Teacher> GetAll()
        {
            var response = db.Select(
                "Lehrer",
                Utils.Collumns("Id", "Name")
            );
            return response.Select(row => new Teacher(row)).ToList();
        }
        public List<Teacher> GetWhereName(string name)
        {
            var response = db.Select(
                "Lehrer",
                Utils.Collumns("Id", "Name"),
                Utils.WhereStatements(("Name", $"%{name}%", WhereChainType.And, WhereComparisonType.Like))
            );
            return response.Select(row => new Teacher(row)).ToList();
        }
        public int Create(int BenutzerID, string Name)
        {
            return db.Create(
                "Lehrer",
                Utils.Collumns("BenutzerId, Name"),
                Utils.Values(BenutzerID, Name)
            );
        }
        public Teachers(Database initializedDb)
        {
            db = initializedDb;
        }
    }
}