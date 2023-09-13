namespace ConsoleFrontend
{
    class StudentPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private int studentId;

        public StudentPage(UserSessionAuth userSessionAuth, OperationsController newOperations, int selectedStudentId)
        {
            auth = userSessionAuth;
            operations = newOperations;
            studentId = selectedStudentId;
        }

        public CommandChain getCommand()
        {
            var student = operations.students.GetById(auth, studentId);
            var classes = operations.classes.GetAllForStudent(auth, studentId);
            switch (classes.Count)
            {
                case 0:
                    return new CommandChain().ReadCommand("Sie sind in noch keiner Klasse eingeschrieben");
                case 1:
                    return new ViewClassForGradesForStudentPage(auth, operations, classes[0], student).getCommand();
                default:
                var navigationTargets = new NavigationTargets();
                foreach (var classe in classes)
                {
                    navigationTargets.AddCommand(
                        classe.Id,
                        () => new ViewClassForGradesForStudentPage(auth, operations, classe, student).getCommand()
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
                        $"Schülerbereich von {student.Name}: ID eingeben um Klasse auszuwählen.",
                        navigationTargets
                    ).SetGoBackReached();
            }
        }
    }
}