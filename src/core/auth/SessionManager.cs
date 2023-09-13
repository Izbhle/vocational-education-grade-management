using DB;
class SessionManager
{
    private NotenDB database;

    public SessionManager(NotenDB db)
    {
        database = db;
    }
    public UserSessionAuth NewSession(string loginId, string password)
    {
        UserForAuth? user = database.users.GetAuthByLoginId(loginId);
        if (user is null) throw new AuthException("Invalid LoginId");
        UserForAuth existingUser = (UserForAuth)user;
        if (existingUser.password == password)
        {
            return new UserSessionAuth(existingUser);
        }
        throw new AuthException("Invalid Password");
    }
}