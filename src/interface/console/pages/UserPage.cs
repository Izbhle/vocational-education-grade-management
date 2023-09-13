namespace ConsoleFrontend
{
    class UserPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public UserPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin) navigationTargets.AddCommandWithLabel(
                () => new AdminPage(auth, operations).getCommand(),
                "Als Admin fortzufahren"
            );
            if (auth.teacherId is not null) navigationTargets.AddCommandWithLabel(
                () => new TeacherPage(auth, operations, (int)auth.teacherId).getCommand(),
                "Als Lehrer fortfahren"
            );
            if (auth.studentId is not null) navigationTargets.AddCommandWithLabel(
                () => new StudentPage(auth, operations, (int)auth.studentId).getCommand(),
                "Als Schüler fortfahren"
            );
            if (navigationTargets.commands.Count == 1)
            {
                return navigationTargets.commands[1]();
            }
            return new CommandChain().Navigate(
                $"Willkommen {auth.loginId}. Wählen Sie aus, wie sie NotenDB verwenden wollen.",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}