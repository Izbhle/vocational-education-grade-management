namespace ConsoleFrontend
{
    class CreateClassPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;

        public CreateClassPage(UserSessionAuth userSessionAuth, OperationsController newOperations)
        {
            auth = userSessionAuth;
            operations = newOperations;
        }

        public CommandChain getCommand()
        {
            return CommandChain.Loop(() => {
                Console.WriteLine("Neuen Klasse anlegen:");
                string name = "";
                return new CommandChain()
                    .ReadString("Name:", ref name)
                    .ExecuteCommand(() => operations.classes.Create(
                        auth,
                        name
                    )).HandleSuccess();
            }).SetGoBackReached();
        }
    }
}