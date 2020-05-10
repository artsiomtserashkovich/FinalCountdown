namespace PrintingMonitor.GCode.Commands.Management
{
    public class GetCurrentPosition : Command
    {
        public GetCurrentPosition() : base(CommandType.M, 114)
        {
        }
    }
}
