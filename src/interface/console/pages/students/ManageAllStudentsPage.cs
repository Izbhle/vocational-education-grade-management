namespace ConsoleFrontend
{
    class ManageAllStudentsPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public ManageAllStudentsPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    var students = operations.students.GetAll(auth);
                    return new ViewStudentsPage(auth, operations, students).getCommand();
                },
                "Alle Schüler anzeigen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    CommandChain command = new CommandChain();
                    string search = "";
                    command = command.ReadString("Wonach soll gesucht werden?", ref search);
                    if (command.GetShouldContinue())
                    {
                        var students = operations.students.GetWhereName(auth, search);
                        return new ViewStudentsPage(auth, operations, students).getCommand();
                    }  
                    return command;
                },
                "Nach Namen suchen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new CreateStudentPage(auth, operations).getCommand(),
                "Neuen Schüler anlegen"
            );
            return new CommandChain().Navigate(
                $"Schülerverwaltung. Was wollen Sie tun?",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}