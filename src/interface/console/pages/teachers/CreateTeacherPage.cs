namespace ConsoleFrontend
{
    class CreateTeacherPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public CreateTeacherPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            return CommandChain.Loop(() => {
                Console.WriteLine("Neuen Lehrer anlegen:");
                string loginId = "";
                string password = "";
                string name = "";
                return new CommandChain()
                    .ReadString("Login ID:", ref loginId)
                    .ReadString("Passwort:", ref password)
                    .ReadString("Name:", ref name)
                    .ExecuteCommand(() => operations.teachers.Create(
                        auth,
                        loginId,
                        password,
                        name
                    )).HandleSuccess();
            }).SetGoBackReached();
        }
    }
}