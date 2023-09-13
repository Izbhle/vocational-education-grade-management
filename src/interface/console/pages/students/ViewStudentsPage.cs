namespace ConsoleFrontend
{
    class ViewStudentsPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private List<Student> students;

        public ViewStudentsPage(UserSessionAuth userSessionAuth, OperationsController newOperations, List<Student> newStudents)
        {
            auth = userSessionAuth;
            operations = newOperations;
            students = newStudents;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            foreach (var student in students)
            {
                navigationTargets.AddCommand(
                    student.Id,
                    () => new ManageStudentPage(auth, operations, student.Id).getCommand()
                );
            }
            navigationTargets.SetCustomLegend(() => {
                Console.WriteLine($"ID   Name");
                foreach(var student in students)
                {
                    Console.WriteLine($"{student.Id.ToString().PadRight(4)} {student.Name}");
                }
            });
            return new CommandChain().Navigate(
                    $"ID eingeben um Schüler auszuwählen.",
                    navigationTargets
                ).SetGoBackReached();
        }
    }
}