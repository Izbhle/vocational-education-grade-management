namespace ConsoleFrontend
{
    class AdminPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public AdminPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new ManageAllStudentsPage(auth, operations).getCommand(),
                "Schüler verwalten"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new ManageAllTeachersPage(auth, operations).getCommand(),
                "Lehrer verwalten"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new ManageAllClassesPage(auth, operations).getCommand(),
                "Klassen verwalten"
            );
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => {
                    Seed seed = new Seed(auth, operations);
                    return new CommandChain().ExecuteCommand(() => {
                        seed.Run();
                    }).HandleSuccess().SetGoBackReached();
                },
                "Testdaten laden"
            );
            return new CommandChain().Navigate(
                $"Systemadministration. Was wollen Sie tun?",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}