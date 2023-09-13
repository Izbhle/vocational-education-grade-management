namespace ConsoleFrontend
{
    class CreateCoursePage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private int classId;

        public CreateCoursePage(UserSessionAuth userSessionAuth, OperationsController newOperations, int newClassId)
        {
            auth = userSessionAuth;
            operations = newOperations;
            classId = newClassId;
        }

        public CommandChain getCommand()
        {
            return CommandChain.Loop(() => {
                Console.WriteLine("Neuen Klasse anlegen:");
                string name = "";
                int teacherId = -1;
                string semester = "";
                return new CommandChain()
                    .ReadString("Name:", ref name)
                    .ReadInt("TeacherId:", ref teacherId)
                    .ReadString("Schuljahr:", ref semester)
                    .ExecuteCommand(() => operations.courses.Create(
                        auth,
                        name,
                        classId,
                        teacherId,
                        semester
                    )).HandleSuccess();
            }).SetGoBackReached();
        }
    }
}