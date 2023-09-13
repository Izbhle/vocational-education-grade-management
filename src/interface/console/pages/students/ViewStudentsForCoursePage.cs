namespace ConsoleFrontend
{
    class ViewStudentsForCoursePage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private List<Student> students;
        private Class classe;
        private Course course;

        public ViewStudentsForCoursePage(
            UserSessionAuth userSessionAuth,
            OperationsController newOperations,
            Class newClasse,
            Course newCourse,
            List<Student> newStudents
        )
        {
            auth = userSessionAuth;
            operations = newOperations;
            students = newStudents;
            classe = newClasse;
            course = newCourse;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            foreach (var student in students)
            {
                navigationTargets.AddCommand(
                    student.Id,
                    () => new ManageStudentForCoursePage(auth, operations, classe, course, student.Id).getCommand()
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
                    $"Schüler von {classe.Name}: {course.Name} {course.Schuljahr} ID eingeben um Schüler auszuwählen.",
                    navigationTargets
                ).SetGoBackReached();
        }
    }
}