namespace PrintingMonitor.Printer.Models.Commands.Management
{
    public class HomingCommand : ManagementCommand
    {
        public HomingCommand(HomingDirection direction)
        {
            Direction = direction;
        }

        public HomingDirection Direction { get; }
    }

    public enum HomingDirection
    {
        X, Y, Z, E, XY, ALL
    }
}
