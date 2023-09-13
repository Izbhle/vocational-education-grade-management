namespace DB 
{
    public class Users
    {
        private Database db;
        public bool CheckIsAdminByLoginId(int loginId)
        {
            var response = db.Select(
                "Benutzer",
                Utils.Collumns("IstAdmin"),
                Utils.WhereStatements(("LoginId", loginId, WhereChainType.And, WhereComparisonType.Equals))
            );
            if (response.Count == 1)
            {
                return (bool)response[0][0];
            }
            return false;
        }
        public int GetIdByLoginId(string loginId)
        {
            var response = db.Select(
                "Benutzer",
                Utils.Collumns("Id"),
                Utils.WhereStatements(("loginId", loginId, WhereChainType.And, WhereComparisonType.Equals))
            );
            if (response.Count == 1)
            {
                return (int)(System.Int64)response[0][0];
            }
            throw new NotFoundException($"User {loginId} not found");
        }
        public int Create(string loginId, string password, bool istAdmin)
        {
            return db.Create(
                "Benutzer",
                Utils.Collumns("LoginId", "Passwort", "IstAdmin"),
                Utils.Values(loginId, password, istAdmin)
            );
        }
        
        public UserForAuth? GetAuthByLoginId(string loginId)
        {
            var response = db.Select(
                "Benutzer",
                Utils.Joins(("Lehrer", "Benutzer"), ("Schueler", "Benutzer")),
                Utils.Collumns(("Benutzer", "LoginId"), ("Benutzer", "Passwort"), ("Benutzer", "IstAdmin"), ("Schueler", "Id"), ("Lehrer", "Id")),
                Utils.WhereStatements(("Benutzer", "loginId", loginId, WhereChainType.And, WhereComparisonType.Equals))
            );
            if (response.Count == 1)
            {
                return new UserForAuth(response[0]);
            }
            return null;
        }
        public Users(Database initializedDb)
        {
            db = initializedDb;
        }
    }
}