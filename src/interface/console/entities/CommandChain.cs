namespace ConsoleFrontend
{
    [System.Serializable]
    public class CommandChainException : System.Exception
    {
        public CommandChainException() { }
        public CommandChainException(string message) : base(message) { }
        public CommandChainException(string message, System.Exception inner) : base(message, inner) { }
        protected CommandChainException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }    
    class CommandChain
    {

        private bool isExit;
        private bool isGoBack;
        private bool isLogout;
        private bool isSuccess;
        public CommandChain()
        {
            isExit = false;
            isGoBack = false;
            isLogout = false;
            isSuccess = false;
        }
        public bool GetShouldExit()
        {
            return isExit;
        }
        public bool GetShouldContinue()
        {
            return (!isExit && !isGoBack && !isLogout);
        }

        public bool GetShouldContinueSession()
        {
            return (!isExit && !isLogout);
        }
        public CommandChain SetGoBackReached()
        {
            isGoBack = false;
            Console.Clear();
            return this;
        }
        public CommandChain SetGoBack()
        {
            isGoBack = true;
            return this;
        }
        public CommandChain SetLogoutReached()
        {
            isLogout = false;
            Console.Clear();
            return this;
        }
        private static bool GetIsSystemCommand(string input)
        {
            if (input.StartsWith(':'))
            {
                return true;
            }
            return false;
        }

        public static void ShowHelpMenu()
        {
            Console.WriteLine("Folgende Befehle sind momentan möglich:");
            Console.WriteLine("':z' um zurückzugehen");
            Console.WriteLine("':b' um das Programm zu beenden");
            Console.WriteLine("':a' um sich von der Sitzung abzumelden");
            Console.WriteLine("':h' für das Hilfsmenü");
        }
        private CommandChain HandleSystemCommand(string input)
        {
            switch (input)
            {
                case (":b"):
                    isExit = true;
                    return this;
                case (":z"):
                    isGoBack = true;
                    return this;
                case (":a"):
                    isLogout = true;
                    return this;
                case (":h"):
                    ShowHelpMenu();
                    throw new CommandChainException("opened help menu");
                default:
                    Console.WriteLine("Unbekannter Befehl.");
                    ShowHelpMenu();
                    throw new CommandChainException("unknown command entered");
            }
        }
        public CommandChain ReadCommand(string text)
        {
            if (!isExit & !isGoBack)
            {
                while (true)
                {
                    Console.WriteLine($"{text} (Nur Befehle mit :)");
                    string? input = Console.ReadLine();
                    if (input is null)
                    {
                    }
                    else if (GetIsSystemCommand(input)) {
                        try 
                        {
                            return HandleSystemCommand(input);
                        }
                        catch (CommandChainException) {}
                    }
                };
            }
            return this;
        }
        public static CommandChain Loop(Func<CommandChain> runCommands)
        {
            var result = runCommands();
            while (result.GetShouldContinue())
            {
                result = runCommands();
            }
            return result;
        }
        public CommandChain Navigate(string title, NavigationTargets options)
        {
            CommandChain result = this;
            while (result.GetShouldContinue())
            {
                Console.Clear();
                Console.WriteLine(title);
                bool validInput = false;
                while (!validInput && result.GetShouldContinue())
                {
                    try
                    {
                        int navigation = 0;
                        options.RenderLegend();
                        result = result.ReadInt("Wählen Sie aus:", ref navigation);
                        if (options.commands.ContainsKey(navigation)) {
                            validInput = true;
                            Console.Clear();
                            result = options.commands[navigation]();
                        }
                        else if (result.GetShouldContinue())
                        {
                            Console.WriteLine("Keine Gültige Option.");
                        }
                    }
                    catch (AccessDeniedException)
                    {
                        Console.WriteLine("Zugriff verweigert");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unbekannter Fehler {ex.Message}");
                    }
                }
            }
            return result;
        }
        public CommandChain Display(Action renderFunc)
        {
            renderFunc();
            return this;
        }
        public CommandChain ExecuteCommand<T>(Func<T> command)
        {
            isSuccess = Response.HandleSuccess(command);
            return this;
        }
        public CommandChain ExecuteCommand(Action command)
        {
            isSuccess = Response.HandleSuccess(command);
            return this;
        }
        public CommandChain HandleSuccess()
        {
            if (isSuccess)
            {
                isSuccess = false;
                isGoBack = true;
            }
            return this;
        }
        public CommandChain ReadInt(string text, ref int value)
        {
            return ReadGeneric<int>($"{text} (Ganze Zahl)", ref value, int.Parse);
        }
        public CommandChain ReadString(string text, ref string value)
        {
            return ReadGeneric<string>($"{text} (Text)", ref value, (s) => s);
        }
        private CommandChain ReadGeneric<T>(string text, ref T value, Func<string, T> parseT)
        {
            if (GetShouldContinue())
            {
                while (true)
                {
                    Console.WriteLine(text);
                    string? input = Console.ReadLine();
                    if (input is null)
                    {
                    }
                    else if (GetIsSystemCommand(input)) {
                        try 
                        {
                            return HandleSystemCommand(input);
                        }
                        catch (CommandChainException) {}
                    }
                    else
                    {
                        try {
                            value = parseT(input);
                            return this;
                        }
                        catch 
                        {
                            Console.WriteLine("Eingabe ungültig. Tippe ':h' um das Hilfsmenü anzuzeigen");
                        }
                    }
                };
            }
            return this;
        }
    }
}