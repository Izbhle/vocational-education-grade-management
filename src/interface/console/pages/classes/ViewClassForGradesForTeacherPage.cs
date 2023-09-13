namespace ConsoleFrontend
{
    class ViewClassForGradesForTeacherPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private Class classe;
        private Teacher teacher;

        public ViewClassForGradesForTeacherPage(UserSessionAuth userSessionAuth, OperationsController newOperations, Class newClass, Teacher newTeacher)
        {
            auth = userSessionAuth;
            operations = newOperations;
            classe = newClass;
            teacher = newTeacher;
        }

        public CommandChain getCommand()
        {
            var courses = operations.courses.GetWhereClassAndTeacher(auth, classe.Id, teacher.Id);
            var navigationTargets = new NavigationTargets();
            foreach (var course in courses)
            {
                navigationTargets.AddCommand(
                    course.Id,
                    () => {
                        var students = operations.students.GetWhereClass(auth, classe.Id);
                        return new ViewStudentsForCoursePage(auth, operations, classe, course, students).getCommand();
                    }
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