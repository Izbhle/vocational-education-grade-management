namespace ConsoleFrontend
{
    class ManageCoursePage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private Class classe;
        private Course course;

        public ManageCoursePage(UserSessionAuth userSessionAuth, OperationsController newOperations, Class selectedClass, Course newCourse)
        {
            auth = userSessionAuth;
            operations = newOperations;
            classe = selectedClass;
            course = newCourse;
        }

        public CommandChain getCommand()
        {
            var navigationTargets = new NavigationTargets();
            if (auth.istAdmin || auth.teacherId is not null) navigationTargets.AddCommandWithLabel(
                () => {
                    var students = operations.students.GetWhereClass(auth, classe.Id);
                    return new ViewStudentsForCoursePage(auth, operations, classe, course, students).getCommand();
                },
                "Alle Schueler anzeigen"
            );
            return new CommandChain().Navigate(
                $"Klasse {classe.Name} - {course.Name} {course.Schuljahr}: Was wollen Sie tun?",
                navigationTargets
            ).SetGoBackReached();
        }
    }
}