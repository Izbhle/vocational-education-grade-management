namespace ConsoleFrontend
{
    class ConsoleEntrypoint
    {
        private SessionManager sessionManager;
        private OperationsController operations;
        public ConsoleEntrypoint(SessionManager newSessionManager, OperationsController newOperations)
        {
            sessionManager = newSessionManager;
            operations = newOperations;
        }
        public void Start()
        {
            var command = new CommandChain();
            while (!command.GetShouldExit())
            {
                Console.WriteLine("Bitte einloggen");
                string loginId = "";
                string password = "";
                command = command.ReadString("Benutzername eingeben:", ref loginId)
                    .ReadString("Passwort eingeben", ref password);
                if (command.GetShouldContinue())
                {
                    try 
                    {
                        var sessionAuth = sessionManager.NewSession(loginId, password);
                        command = new UserPage(sessionAuth, operations).getCommand();
                    }
                    catch (AuthException)
                    {
                        Console.WriteLine("Login Fehlgeschlagen");
                    }
                }
                command = command.SetGoBackReached().SetLogoutReached();
            }
        }
    }
}