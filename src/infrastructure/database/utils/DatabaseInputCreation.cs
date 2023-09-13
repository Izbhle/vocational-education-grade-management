namespace DB
{
    public static class Utils
    {
        public static string[] Collumns(params string[] names)
        {
            return names;
        }
        public static (string, string)[] Collumns(params (string, string)[] namesWithTable)
        {
            return namesWithTable;
        }
        public static (string, string)[] Joins(params (string, string)[] joins)
        {
            return joins;
        }
        public static (string, object, WhereChainType, WhereComparisonType)[] WhereStatements(params (string, object, WhereChainType, WhereComparisonType)[] whereStatements)
        {
            return whereStatements;
        }
        public static (string, string, object, WhereChainType, WhereComparisonType)[] WhereStatements(params (string, string, object, WhereChainType, WhereComparisonType)[] whereStatements)
        {
            return whereStatements;
        }
        public static object[] Values(params object[] values)
        {
            return values;
        }
    }
}