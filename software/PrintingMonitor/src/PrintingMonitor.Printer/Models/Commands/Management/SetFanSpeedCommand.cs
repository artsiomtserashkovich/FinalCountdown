namespace PrintingMonitor.Printer.Models.Commands.Management
{
    public class SetFanSpeedCommand : ManagementCommand
    {
        public SetFanSpeedCommand(int speed)
        {
            Speed = speed;
        }

        public int Speed { get; }
    }
}
