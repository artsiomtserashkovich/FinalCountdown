namespace PrintingMonitor.GCode.Commands.Management.Printing
{
    public class StopPrint : Command
    {
        public StopPrint() : base(CommandType.M, 25) { }
    }
}
