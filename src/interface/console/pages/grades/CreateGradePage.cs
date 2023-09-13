namespace ConsoleFrontend
{
    class CreateGradesPage
    {
        private UserSessionAuth auth;
        private OperationsController operations;
        private int studentId;
        private int courseId;
        private int noteTypeId;

        public CreateGradesPage(UserSessionAuth userSessionAuth, OperationsController newOperations, int newStudentId, int newCourseId, int newNoteTypeId)
        {
            auth = userSessionAuth;
            operations = newOperations;
            studentId = newStudentId;
            courseId = newCourseId;
            noteTypeId = newNoteTypeId;
        }

        public CommandChain getCommand()
        {
            return CommandChain.Loop(() => {
                Console.WriteLine("Neuen Note Eintragen:");
                string grade = "";
                string date = "";
                return new CommandChain()
                    .ReadString("Note:", ref grade)
                    .ReadString("Datum:", ref date)
                    .ExecuteCommand(() => operations.grades.Create(
                        auth,
                        studentId,
                        courseId,
                        grade,
                        date,
                        noteTypeId
                    )).HandleSuccess();
            }).SetGoBackReached();
        }
    }
}