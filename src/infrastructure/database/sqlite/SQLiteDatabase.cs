using System.Data.SQLite;

namespace DB
{
    public class SQLiteDB: Database
    {
        private string PrepareObjectForSQLiteCode(object value)
        {
            switch (value)
            {
                case Boolean:
                    return $"{value}";
                case System.Int64:
                    return $"{value}";
                case Double:
                    return $"{value}";
                default:
                    return $"'{value}'";
            }
        }
        private string PrepareWhereComparisonForSQLiteCode(WhereComparisonType value)
        {
            switch (value)
            {
                case WhereComparisonType.Equals:
                    return "=";
                case WhereComparisonType.Like:
                    return "LIKE";
                case WhereComparisonType.Is:
                    return "IS";
                default:
                    return "=";
            }
        }
        private string PrepareWhereChainForSQLiteCode(WhereChainType value)
        {
            switch (value)
            {
                case WhereChainType.And:
                    return "AND";
                case WhereChainType.Or:
                    return "OR";
                default:
                    return "AND";
            }
        }
        private string PrepareWhereForSQLiteCode((string, object, WhereChainType, WhereComparisonType)[] whereStatements)
        {
            string SQLCode = $"{whereStatements[0].Item1} {PrepareWhereComparisonForSQLiteCode(whereStatements[0].Item4)} {PrepareObjectForSQLiteCode(whereStatements[0].Item2)}";

            for (int i = 1; i < whereStatements.Length; i++)
            {
                SQLCode = SQLCode + $" {PrepareWhereChainForSQLiteCode(whereStatements[i-1].Item3)} {whereStatements[i].Item1} {PrepareWhereComparisonForSQLiteCode(whereStatements[i].Item4)} {PrepareObjectForSQLiteCode(whereStatements[i].Item2)}";
            }
            return SQLCode;
        }
        private string PrepareWhereForSQLiteCode((string, string, object, WhereChainType, WhereComparisonType)[] whereStatements)
        {
            string SQLCode = $"{whereStatements[0].Item1}.{whereStatements[0].Item2} {PrepareWhereComparisonForSQLiteCode(whereStatements[0].Item5)} {PrepareObjectForSQLiteCode(whereStatements[0].Item3)}";

            for (int i = 1; i < whereStatements.Length; i++)
            {
                SQLCode = SQLCode + $" {PrepareWhereChainForSQLiteCode(whereStatements[i-1].Item4)} {whereStatements[i].Item1}.{whereStatements[i].Item2} {PrepareWhereComparisonForSQLiteCode(whereStatements[i].Item5)} {PrepareObjectForSQLiteCode(whereStatements[i].Item3)}";
            }
            return SQLCode;
        }
        private List<object[]> SelectResponseFromSQLCode(string sqlCode, int length)
        {
            var command = new SQLiteCommand(sqlCode, connection);
            var queryResult = command.ExecuteReader();
            List<object[]> response = new List<object[]>();
            while (queryResult.Read())
            {
                object[] row = new object[length];
                queryResult.GetValues(row);
                response.Add(row);
            }
            return response;
        }
        public SQLiteConnection connection;
        public SQLiteDB(string dbFilePath, string migrationsPath)
        {
            string cs = $"URI=file:./{dbFilePath}";
            connection = new SQLiteConnection(cs);
            connection.Open();
            Console.WriteLine($"Loading DB from {dbFilePath}...");
            EnsureTableStructure();
            ApplyPendingMigrations(migrationsPath);
            Console.WriteLine("Successfully loaded DB");
        }
        public List<object[]> Select(string tableName, string[] collumns)
        {
            string sqlCode = $"SELECT DISTINCT {string.Join(", ", collumns)} FROM {tableName}";
            return SelectResponseFromSQLCode(sqlCode, collumns.Length);
        }
        public List<object[]> Select(string tableName, string[] collumns, (string, object, WhereChainType, WhereComparisonType)[] whereStatements)
        {
            var where = PrepareWhereForSQLiteCode(whereStatements);
            string sqlCode = $"SELECT DISTINCT {string.Join(", ", collumns)} FROM {tableName} WHERE {where}";
            return SelectResponseFromSQLCode(sqlCode, collumns.Length);
        }
        public List<object[]> Select(string tableName, (string, string)[] joins, (string, string)[] collumns)
        {
            string[] collumnsSqlCode = collumns.Select(collumn => $"{collumn.Item1}.{collumn.Item2}").ToArray();
            string[] joinCommands = joins.Select(joins => $"LEFT JOIN {joins.Item1} ON ({joins.Item1}.{joins.Item2}ID = {joins.Item2}.ID)").ToArray();
            string sqlCode = $"SELECT DISTINCT {string.Join(", ", collumnsSqlCode)} FROM {tableName} {string.Join(" ", joinCommands)}";
            return SelectResponseFromSQLCode(sqlCode, collumns.Length);


        }
        public List<object[]> Select(string tableName, (string, string)[] joins, (string, string)[] collumns, (string, string, object, WhereChainType, WhereComparisonType)[] whereStatements)
        {
            var where = PrepareWhereForSQLiteCode(whereStatements);
            string[] collumnsSqlCode = collumns.Select(collumn => $"{collumn.Item1}.{collumn.Item2}").ToArray();
            string[] joinCommands = joins.Select(joins => $"LEFT JOIN {joins.Item1} ON ({joins.Item1}.{joins.Item2}ID = {joins.Item2}.ID)").ToArray();
            string sqlCode = $"SELECT DISTINCT {string.Join(", ", collumnsSqlCode)} FROM {tableName} {string.Join(" ", joinCommands)} WHERE {where}";
            return SelectResponseFromSQLCode(sqlCode, collumns.Length);
        }
        public int Create(string tableName, string[] collumns, object[] values)
        {
            string[] valuesForSQLite = values.Select(value => PrepareObjectForSQLiteCode(value)).ToArray();
            string sqlCode = $"INSERT INTO {tableName} ({string.Join(", ", collumns)}) VALUES({string.Join(", ", valuesForSQLite)}); SELECT last_insert_rowid();";
            var command = new SQLiteCommand(sqlCode, connection);
            return (int)(System.Int64)(command.ExecuteScalar());
        }
        public int Delete(string tableName, int id)
        {
            string sqlCode = $"UPDATE {tableName} SET IstGeloescht = TRUE WHERE Id = {id}";
            var command = new SQLiteCommand(sqlCode, connection);
            command.ExecuteNonQuery();
            return (int)(System.Int64)(command.ExecuteNonQuery());
        }
        private void EnsureTableStructure()
        {
            if (!HasTableStructure())
            {
                Console.WriteLine($"Empty DB detected. Creating table structure...");
                SQLiteCommand cmd = new SQLiteCommand($"CREATE TABLE sdmdb_migrations (name VARCHAR PRIMARY KEY);", connection);
                cmd.ExecuteReader();
            }
            return;
        }
        private Boolean HasTableStructure()
        {
            SQLiteCommand cmd = new SQLiteCommand($"SELECT name FROM sqlite_master WHERE type='table' AND name='sdmdb_migrations';", connection);
            var result = cmd.ExecuteReader();
            return result.HasRows;
        }
        private void ApplyPendingMigrations(string migrationsPath)
        {
            // Get migration files from directory
            List<string> migrationFiles = Directory.GetFiles(migrationsPath).Select(file => Path.GetFileName(file)).ToList();
            migrationFiles.Sort();

            // Load all migrations in the db
            SQLiteCommand cmd = new SQLiteCommand($"SELECT name FROM sdmdb_migrations;", connection);
            var queryResult = cmd.ExecuteReader();
            List<string> migrationsInDB = new List<string>();
            while(queryResult.Read())
            {
                migrationsInDB.Add((string)queryResult.GetValue(0));
            }
            
            // Check for pending migrations
            string[] pendingMigrations = migrationFiles.Where(migration => !(migrationsInDB.Contains(migration))).ToArray();
            foreach (string migrationFile in pendingMigrations)
            {
                ApplyMigrationFile(migrationsPath, migrationFile);
            }
        }
        private void ApplyMigrationFile(string migrationsPath, string migrationFile)
        {
            Console.WriteLine($"Appliying migration {migrationFile}...");
            string schema = File.ReadAllText(Path.Join(migrationsPath, migrationFile));
            new SQLiteCommand(schema, connection).ExecuteReader();
            // Mark migration as completed in the DB
            new SQLiteCommand($"INSERT INTO sdmdb_migrations (name) VALUES('{migrationFile}');", connection).ExecuteReader();
        }
    }
}