namespace PrintingMonitor.GCode.Commands.Management.Temperature
{
    public class ReportTemperatures : Command
    {
        public ReportTemperatures() : base(CommandType.M, 105, null, false) { }
    }
}
