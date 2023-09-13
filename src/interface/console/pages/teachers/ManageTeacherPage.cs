namespace ConsoleFrontend
{
    class ManageTeacherPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private int teacherId;

        public ManageTeacherPage(UserSessionAuth userSessionAuth, OperationsController newOperations, int selectedTeacherId)
        {
            auth = userSessionAuth;
            operations = newOperations;
            teacherId = selectedTeacherId;
        }
        public CommandChain getCommand()
        {
            var teacher = operations.teachers.GetById(auth, teacherId);
            Console.WriteLine($"{teacher.Name}:");
            return new CommandChain();
        }
    }
}