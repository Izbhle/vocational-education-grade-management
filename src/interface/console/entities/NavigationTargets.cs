namespace ConsoleFrontend
{
    class NavigationTargets
    {
        public Dictionary<int, Func<CommandChain>> commands;
        private Dictionary<int, string> legends;
        private int commandNumber;
        private Action? customRenderAction;
        public NavigationTargets()
        {
            commandNumber = 0;
            commands = new Dictionary<int, Func<CommandChain>>();
            legends = new Dictionary<int, string>();
        }
        public void AddCommand(int navigation, Func<CommandChain> command)
        {
            commands[navigation] = command;
        }
        public void AddCommand(Func<CommandChain> command)
        {
            commandNumber += 1;
            commands[commandNumber] = command;

        }
        public void AddCommandWithLabel(Func<CommandChain> command, string label)
        {
            commandNumber += 1;
            commands[commandNumber] = command;
            legends[commandNumber] = $"'{commandNumber}': {label}";

        }
        public void AddCommandWithLabel(int navigation, Func<CommandChain> command, string label)
        {
            commands[navigation] = command;
            legends[navigation] = $"'{navigation}': {label}";

        }
        public void SetCustomLegend(Action renderFunc)
        {
            customRenderAction = renderFunc;
        }
        public void RenderLegend()
        {
            if (customRenderAction is null)
            {
                foreach (var label in legends)
                {
                    Console.WriteLine(label.Value);
                }
            }
            else
            {
                customRenderAction();
            }
        }
    }
}