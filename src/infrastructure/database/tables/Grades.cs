namespace DB 
{
    public class Grades
    {
        private Database db;
        
        public int Create(
            int studentId,
            int courseId,
            string grade,
            string date,
            int noteTypeId
        )
        {
            return db.Create(
                "Note",
                Utils.Collumns("SchuelerId", "KursId", "Schulnote", "Datum", "NotenTypId"),
                Utils.Values(
                    studentId,
                    courseId,
                    grade,
                    date,
                    noteTypeId
                )
            );
        }
        public int Delete(int gradeId)
        {
            return db.Delete("Note", gradeId);
        }
        public List<Grade> GetWhereStudentAndCourse(int studentId, int courseId)
        {
            var response = db.Select(
                "NotenTyp",
                Utils.Joins(("Note", "NotenTyp")),
                Utils.Collumns(("Note", "Id"), ("Note", "Schulnote"), ("NotenTyp", "Name"), ("Note", "SchuelerId"), ("Note", "KursId"), ("Note", "Datum")),
                Utils.WhereStatements(
                    ("Note", "SchuelerId", studentId, WhereChainType.And, WhereComparisonType.Equals),
                    ("Note", "KursId", courseId, WhereChainType.And, WhereComparisonType.Equals),
                    ("Note", "IstGeloescht", false, WhereChainType.And, WhereComparisonType.Is)
                )
            );
            return response.Select(row => new Grade(row)).ToList();
        }
        public Grade GetById(int gradeId)
        {
            var response = db.Select(
                "NotenTyp",
                Utils.Joins(("Note", "NotenTyp")),
                Utils.Collumns(("Note", "Id"), ("Note", "Schulnote"), ("NotenTyp", "Name"), ("Note", "SchuelerId"), ("Note", "KursId"), ("Note", "Datum")),
                Utils.WhereStatements(
                    ("Note", "Id", gradeId, WhereChainType.And, WhereComparisonType.Equals),
                    ("Note", "IstGeloescht", false, WhereChainType.And, WhereComparisonType.Is)
                )
            );
            return new Grade(response[0]);
        }
        public Grades(Database initializedDb)
        {
            db = initializedDb;
        }
    }
}