struct UserSessionAuth
{
    public string loginId;
    public int? studentId;
    public int? teacherId;
    public bool istAdmin;
    public UserSessionAuth(UserForAuth user)
    {
        loginId = user.LoginId;
        studentId = user.SchuelerId;
        teacherId = user.LehrerId;
        istAdmin = user.IstAdmin;
    }
}