namespace DB
{
    public enum WhereChainType
    {
        And,
        Or

    }
    public enum WhereComparisonType
    {
        Equals,
        Like,
        Is,

    }    
    public interface Database
    {

        public List<object[]> Select(string tableName, string[] collumns);
        public List<object[]> Select(string tableName, string[] collumns, (string, object, WhereChainType, WhereComparisonType)[] whereStatements);
        public List<object[]> Select(string tableName, (string, string)[] joins, (string, string)[] collumns);
        public List<object[]> Select(string tableName, (string, string)[] joins, (string, string)[] collumns, (string, string, object, WhereChainType, WhereComparisonType)[] whereStatements);
        public int Create(string tableName, string[] collumns, object[] values);
        public int Delete(string tableName, int id);
    }
}