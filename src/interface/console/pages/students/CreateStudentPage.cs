namespace ConsoleFrontend
{
    class CreateStudentPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public CreateStudentPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            return CommandChain.Loop(() => {
                Console.WriteLine("Neuen Schüler anlegen:");
                string loginId = "";
                string password = "";
                string name = "";
                return new CommandChain()
                    .ReadString("Login ID:", ref loginId)
                    .ReadString("Passwort:", ref password)
                    .ReadString("Name:", ref name)
                    .ExecuteCommand(() => operations.students.Create(
                        auth,
                        loginId,
                        password,
                        name
                    )).HandleSuccess();
            }).SetGoBackReached();
        }
    }
}