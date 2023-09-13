namespace ConsoleFrontend
{
    class ViewCoursesForClassPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private Class classe;

        public ViewCoursesForClassPage(UserSessionAuth userSessionAuth, OperationsController newOperations, Class newClass)
        {
            auth = userSessionAuth;
            operations = newOperations;
            classe = newClass;
        }

        public CommandChain getCommand()
        {
            var courses = operations.courses.GetWhereClass(auth, classe.Id);
            var navigationTargets = new NavigationTargets();
            foreach (var course in courses)
            {
                navigationTargets.AddCommand(
                    course.Id,
                    () => new ManageCoursePage(auth, operations, classe, course).getCommand()
                );
            }
            navigationTargets.SetCustomLegend(() => {
                Console.WriteLine($"ID   Name   Schuljahr");
                foreach(var course in courses)
                {
                    Console.WriteLine($"{course.Id.ToString().PadRight(4)} {course.Name.PadRight(6)} {course.Schuljahr}");
                }
            });
            return new CommandChain().Navigate(
                    $"Kurse von {classe.Name}. ID eingeben um Kurs auszuwählen.",
                    navigationTargets
                ).SetGoBackReached();
        }
    }
}