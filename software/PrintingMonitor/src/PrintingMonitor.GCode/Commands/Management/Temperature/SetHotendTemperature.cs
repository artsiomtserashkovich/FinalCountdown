namespace PrintingMonitor.GCode.Commands.Management.Temperature
{
    public class SetHotendTemperature : Command
    {
        public SetHotendTemperature(int temperature) : base(CommandType.M, 104, new[] { $"S{temperature}" }) { }
    }
}
