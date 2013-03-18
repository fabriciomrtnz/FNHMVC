namespace FNHMVC.CommandProcessor.Command
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success)
        {
            this.Success = success;
        }

        public bool Success { get; protected set; }
    }
}

