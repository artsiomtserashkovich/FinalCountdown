namespace PrintingMonitor.Printer.Models.Commands.Management
{
    public class MoveCommand : ManagementCommand
    {
        public MoveDirection Direction { get; }

        public MoveCommand(MoveDirection direction, double length)
        {
            Direction = direction;
            Length = length;
        }

        public MoveDirection MoveDirection { get; private set; }

        public double Length { get; private set; }
    }

    public enum MoveDirection
    {
        Up, Down, Left, Right
    }
}
