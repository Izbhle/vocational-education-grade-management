using DB;

[System.Serializable]
public class AccessDeniedException : System.Exception
{
    public AccessDeniedException() { }
    public AccessDeniedException(string message) : base(message) { }
    public AccessDeniedException(string message, System.Exception inner) : base(message, inner) { }
    protected AccessDeniedException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
class AccessManager
{
    private NotenDB database;

    public AccessManager(NotenDB db)
    {
        database = db;
    }
    public void CheckAdminAccess(UserSessionAuth sessionAuth)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);

        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        throw new AccessDeniedException("Admin access denied");
    }
    public void CheckTeacherAccess(UserSessionAuth sessionAuth)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);

        if (userInDB?.LehrerId is not null && userInDB?.LehrerId == sessionAuth.teacherId)
        {
            return;
        }
        throw new AccessDeniedException("Teacher access denied");
    }
    public void CheckTeacherOrAdminAccess(UserSessionAuth sessionAuth)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (userInDB?.LehrerId is not null && userInDB?.LehrerId == sessionAuth.teacherId)
        {
            return;
        }
        throw new AccessDeniedException("Teacher access denied");
    }
    public void CheckSpecificStudentOrAdminAccess(UserSessionAuth sessionAuth, int? studentId)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (userInDB?.SchuelerId is not null && userInDB?.SchuelerId == sessionAuth.studentId && sessionAuth.studentId == studentId)
        {
            return;
        }
        throw new AccessDeniedException("Student access denied");
    }
    public void CheckSpecificStudentOrTeacherOrAdminAccess(UserSessionAuth sessionAuth, int? studentId)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (userInDB?.LehrerId is not null)
        {
            return;
        }
        if (userInDB?.SchuelerId is not null && userInDB?.SchuelerId == sessionAuth.studentId && sessionAuth.studentId == studentId)
        {
            return;
        }
        throw new AccessDeniedException("Student access denied");
    }
    public void CheckSpecificTeacherOrAdminAccess(UserSessionAuth sessionAuth, int? teacherId)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (userInDB?.LehrerId is not null && userInDB?.LehrerId == sessionAuth.teacherId && sessionAuth.teacherId == teacherId)
        {
            return;
        }
        throw new AccessDeniedException("Teacher access denied");
    }
    public void CheckCourseTeacherOrAdminAccess(UserSessionAuth sessionAuth, int courseId)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (userInDB?.LehrerId is not null && userInDB?.LehrerId == sessionAuth.teacherId)
        {
            Course course = database.courses.GetById(courseId);
            if (course.LehrerId == userInDB?.LehrerId)
            {
                return;
            }
        }
        throw new AccessDeniedException("Teacher access denied");
    }
    public void CheckCourseMemberOrAdminAccess(UserSessionAuth sessionAuth, int courseId)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (userInDB is not null)
        {
            Course course = database.courses.GetById(courseId);
            if (course.LehrerId == userInDB?.LehrerId)
            {
                return;
            }
            int? enrollment = database.enrollments.GetWhereStudentAndClass(
                userInDB?.SchuelerId,
                course.KlasseId
            );
            if (enrollment is not null)
            {
                return;
            }
        }
        throw new AccessDeniedException("Teacher or student access denied");
    }

    public void CheckSpecificStudentOrCourseTeacherOrAdminAccess(UserSessionAuth sessionAuth, int courseId, int? studentId)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (studentId is not null && userInDB?.SchuelerId == studentId)
        {
            return;
        }
        if (userInDB is not null)
        {
            Course course = database.courses.GetById(courseId);
            if (course.LehrerId == userInDB?.LehrerId)
            {
                return;
            }
        }
        throw new AccessDeniedException("Teacher or student access denied");
    }
        public void CheckTeacherOrAdminOrClassMemberAccess(UserSessionAuth sessionAuth, int classId)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);
        if (userInDB?.IstAdmin is true)
        {
            return;
        }
        if (userInDB?.LehrerId is not null && userInDB?.LehrerId == sessionAuth.teacherId)
        {
            return;
        }
        if (userInDB is not null)
        {
            int? enrollment = database.enrollments.GetWhereStudentAndClass(
                userInDB?.SchuelerId,
                classId
            );
            if (enrollment is not null)
            {
                return;
            }
        }
        throw new AccessDeniedException("Teacher access denied");
    }
    public void CheckStudentAccess(UserSessionAuth sessionAuth)
    {
        var userInDB = database.users.GetAuthByLoginId(sessionAuth.loginId);

        if (userInDB?.SchuelerId is not null && userInDB?.SchuelerId == sessionAuth.studentId)
        {
            return;
        }
        throw new AccessDeniedException("Student access denied");
    }
    
}