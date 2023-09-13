namespace ConsoleFrontend
{
    class ManageClassPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private Class classe;

        public ManageClassPage(UserSessionAuth userSessionAuth, OperationsController newOperations, Class newClass)
        {
            auth = userSessionAuth;
            operations = newOperations;
            classe = newClass;
        }
        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin || auth.teacherId is not null) navigationTargets.AddCommandWithLabel(
                () => {
                    var students = operations.students.GetWhereClass(auth, classe.Id);
                    return new ViewStudentsPage(auth, operations, students).getCommand();
                },
                "Alle Schueler anzeigen"
            );
            if (auth.istAdmin || auth.teacherId is not null) navigationTargets.AddCommandWithLabel(
                () => {
                    return new ViewCoursesForClassPage(auth, operations, classe).getCommand();
                },
                "Alle Kurse anzeigen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    CommandChain command = new CommandChain();
                    int studentId = -1;
                    return new CommandChain()
                        .ReadInt("SchülerId eigeben:", ref studentId)
                        .ExecuteCommand(() => operations.classes.AddStudentToClass(
                            auth,
                            studentId,
                            classe.Id
                        )).HandleSuccess();
                },
                "Schüler zur Klasse hinzufügen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new CreateCoursePage(auth, operations, classe.Id).getCommand(),
                "Neuen Kurs anlegen"
            );
            return new CommandChain().Navigate(
                $"Klasse {classe.Name}: Was wollen Sie tun?",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}