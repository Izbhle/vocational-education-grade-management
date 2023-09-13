namespace ConsoleFrontend
{
    class ManageStudentForCoursePage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private int studentId;
        private Class classe;
        private Course course;

        public ManageStudentForCoursePage(
            UserSessionAuth userSessionAuth,
            OperationsController newOperations,
            Class newClasse,
            Course newCourse,
            int selectedStudentId
        )
        {
            auth = userSessionAuth;
            operations = newOperations;
            studentId = selectedStudentId;
            classe = newClasse;
            course = newCourse;
        }
        public CommandChain getCommand()
        {
            var student = operations.students.GetById(auth, studentId);
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin || auth.teacherId is not null) navigationTargets.AddCommandWithLabel(
                () => {
                    return new ViewGradesForStudentsAndCoursePage(auth, operations, student, course).getCommand();
                },
                "Alle Noten anzeigen"
            );
            if (auth.istAdmin || auth.teacherId is not null) navigationTargets.AddCommandWithLabel(
                () => {
                    return new CreateGradesPage(auth, operations, studentId, course.Id, 1).getCommand();
                },
                "Neue Klausurnote eintragen"
            );
            if (auth.istAdmin || auth.teacherId is not null) navigationTargets.AddCommandWithLabel(
                () => {
                    return new CreateGradesPage(auth, operations, studentId, course.Id, 2).getCommand();
                },
                "Neue Sominote eintragen"
            );
            return new CommandChain().Navigate(
                $"{classe.Name}: {course.Name} {course.Schuljahr}: {student.Name}: Was wollen Sie tun?",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}