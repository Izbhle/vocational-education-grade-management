namespace ConsoleFrontend
{
    class ManageStudentPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private int studentId;

        public ManageStudentPage(UserSessionAuth userSessionAuth, OperationsController newOperations, int selectedStudentId)
        {
            auth = userSessionAuth;
            operations = newOperations;
            studentId = selectedStudentId;
        }
        public CommandChain getCommand()
        {
            var student = operations.students.GetById(auth, studentId);
            Console.WriteLine($"{student.Name}:");
            return new CommandChain();
        }
    }
}