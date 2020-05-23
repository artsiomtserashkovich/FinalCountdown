namespace PrintingMonitor.Printer.Models.Commands.Management
{
    public class MoveCommand : ManagementCommand
    {
        public MoveCommand(MoveDirection direction, double length)
        {
            Direction = direction;
            Length = length;
        }

        public MoveDirection Direction { get; }

        public double Length { get; private set; }
    }

    public enum MoveDirection
    {
        Up, Down, Left, Right, ExtruderUp, ExtruderDown, ZUp, ZDown
    }
}
