namespace ConsoleFrontend
{
    class ViewGradesForStudentsAndCoursePage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private Student student;
        private Course course;

        public ViewGradesForStudentsAndCoursePage(UserSessionAuth userSessionAuth, OperationsController newOperations, Student newStudent, Course newCourse)
        {
            auth = userSessionAuth;
            operations = newOperations;
            student = newStudent;
            course = newCourse;
        }

        public CommandChain getCommand()
        {
            var grades = operations.grades.GetWhereStudentAndCourse(auth, student.Id, course.Id);
            var navigationTargets = new NavigationTargets();
            foreach (var grade in grades)
            {
                navigationTargets.AddCommand(
                    grade.Id,
                    () => {
                        return new CommandChain()
                            .ExecuteCommand(() => operations.grades.Delete(
                                auth,
                                grade.Id
                            )).HandleSuccess();
                    }
                );
            }
            navigationTargets.SetCustomLegend(() => {
                Console.WriteLine($"ID   NotenTyp     Schulnote Datum");
                foreach(var grade in grades)
                {
                    Console.WriteLine($"{grade.Id.ToString().PadRight(4)} {grade.NotenTyp.PadRight(12)} {grade.Schulnote.PadRight(9)} {grade.Datum}");
                }
            });
            string title;
            if (auth.teacherId is not null)
            {
                title = $"Noten von {student.Name} im Kurs {course.Name} {course.Schuljahr} ID eingeben um Note zu löschen.";
            }
            else
            {
                title = $"Noten von {student.Name} im Kurs {course.Name} {course.Schuljahr}";
            }
            return new CommandChain().Navigate(
                    title,
                    navigationTargets
                ).SetGoBackReached();
        }
    }
}