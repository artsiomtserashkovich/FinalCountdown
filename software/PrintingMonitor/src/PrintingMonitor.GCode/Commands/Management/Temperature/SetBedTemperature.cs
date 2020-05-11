namespace PrintingMonitor.GCode.Commands.Management.Temperature
{
    public class SetBedTemperature : Command
    {
        public SetBedTemperature(int temperature) : base(CommandType.M, 140, new[] { $"S{temperature}" }) { }
    }
}
