namespace DB 
{
    public class Enrollments
    {
        private Database db;
        
        public int Create(int studentId, int classId)
        {
            return db.Create(
                "Einschreibung",
                Utils.Collumns("SchuelerId", "KlasseId"),
                Utils.Values(studentId, classId)
            );
        }
        public int? GetWhereStudentAndClass(int? studentId, int? classId)
        {
            if (studentId is null || classId is null)
            {
                return null;
            }
            var response = db.Select(
                "Einschreibung",
                Utils.Collumns("Id"),
                Utils.WhereStatements(
                    ("KlasseId", classId, WhereChainType.And, WhereComparisonType.Equals),
                    ("SchuelerId", studentId, WhereChainType.And, WhereComparisonType.Equals)
                )
            );
            if (response.Count == 1)
            {
                return (int)(System.Int64)response[0][0];
            }
            return null;
        }
        public Enrollments(Database initializedDb)
        {
            db = initializedDb;
        }
    }
}