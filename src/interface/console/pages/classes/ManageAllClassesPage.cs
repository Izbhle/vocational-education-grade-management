namespace ConsoleFrontend
{
    class ManageAllClassesPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public ManageAllClassesPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    var classes = operations.classes.GetAll(auth);
                    return new ViewClassesPage(auth, operations, classes).getCommand();
                },
                "Alle Klassen anzeigen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    CommandChain command = new CommandChain();
                    string search = "";
                    command = command.ReadString("Wonach soll gesucht werden?", ref search);
                    if (command.GetShouldContinue())
                    {
                        var classes = operations.classes.GetWhereName(auth, search);
                        return new ViewClassesPage(auth, operations, classes).getCommand();
                    }  
                    return command;
                },
                "Nach Namen suchen"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new CreateClassPage(auth, operations).getCommand(),
                "Neuen Klasse anlegen"
            );
            return new CommandChain().Navigate(
                $"Klassenverwaltung. Was wollen Sie tun?",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}