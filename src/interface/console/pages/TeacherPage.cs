namespace ConsoleFrontend
{
    class TeacherPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private int teacherId;

        public TeacherPage(UserSessionAuth userSessionAuth, OperationsController newOperations, int selectedTeacherId)
        {
            auth = userSessionAuth;
            operations = newOperations;
            teacherId = selectedTeacherId;
        }

        public CommandChain getCommand()
        {
            var teacher = operations.teachers.GetById(auth, teacherId);
            var classes = operations.classes.GetAllForTeacher(auth, teacherId);
            switch (classes.Count)
            {
                case 0:
                    return new CommandChain().ReadCommand("Sie haben noch keine Kurse");
                default:
                var navigationTargets = new NavigationTargets();
                foreach (var classe in classes)
                {
                    navigationTargets.AddCommand(
                        classe.Id,
                        () => new ViewClassForGradesForTeacherPage(auth, operations, classe, teacher).getCommand()
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
                        $"Lehrerbereich von {teacher.Name}: ID eingeben um Klasse auszuwählen.",
                        navigationTargets
                    ).SetGoBackReached();
            }
        }
    }
}