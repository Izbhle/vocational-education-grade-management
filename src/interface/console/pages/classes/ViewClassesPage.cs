namespace ConsoleFrontend
{
    class ViewClassesPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private List<Class> classes;

        public ViewClassesPage(UserSessionAuth userSessionAuth, OperationsController newOperations, List<Class> newClasses)
        {
            auth = userSessionAuth;
            operations = newOperations;
            classes = newClasses;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            foreach (var classe in classes)
            {
                navigationTargets.AddCommand(
                    classe.Id,
                    () => new ManageClassPage(auth, operations, classe).getCommand()
                );
            }
            navigationTargets.SetCustomLegend(() => {
                Console.WriteLine($"ID   Name");
                foreach(var classe in classes)
                {
                    Console.WriteLine($"{classe.Id.ToString().PadRight(4)} {classe.Name}");
                }
            });
            return new CommandChain().Navigate(
                    $"ID eingeben um Klasse auszuwählen.",
                    navigationTargets
                ).SetGoBackReached();
        }
    }
}