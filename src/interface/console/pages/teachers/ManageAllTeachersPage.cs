namespace ConsoleFrontend
{
    class ManageAllTeachersPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public ManageAllTeachersPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    var teachers = operations.teachers.GetAll(auth);
                    return new ViewTeachersPage(auth, operations, teachers).getCommand();
                },
                "Alle Lehrer anzeigen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    CommandChain command = new CommandChain();
                    string search = "";
                    command = command.ReadString("Wonach soll gesucht werden?", ref search);
                    if (command.GetShouldContinue())
                    {
                        var teachers = operations.teachers.GetWhereName(auth, search);
                        return new ViewTeachersPage(auth, operations, teachers).getCommand();
                    }  
                    return command;
                },
                "Nach Namen suchen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new CreateTeacherPage(auth, operations).getCommand(),
                "Neuen Lehrer anlegen"
            );
            return new CommandChain().Navigate(
                $"Lehrerverwaltung. Was wollen Sie tun?",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}