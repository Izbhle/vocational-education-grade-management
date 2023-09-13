namespace ConsoleFrontend
{
    class ViewTeachersPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private List<Teacher> teachers;

        public ViewTeachersPage(UserSessionAuth userSessionAuth, OperationsController newOperations, List<Teacher> newTeachers)
        {
            auth = userSessionAuth;
            operations = newOperations;
            teachers = newTeachers;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            foreach (var teacher in teachers)
            {
                navigationTargets.AddCommand(
                    teacher.Id,
                    () => new ManageTeacherPage(auth, operations, teacher.Id).getCommand()
                );
            }
            navigationTargets.SetCustomLegend(() => {
                Console.WriteLine($"ID   Name");
                foreach(var teacher in teachers)
                {
                    Console.WriteLine($"{teacher.Id.ToString().PadRight(4)} {teacher.Name}");
                }
            });
            return new CommandChain().Navigate(
                    $"ID eingeben um Lehrer auszuwählen.",
                    navigationTargets
                ).SetGoBackReached();
        }
    }
}