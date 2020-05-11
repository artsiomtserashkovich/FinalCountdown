namespace PrintingMonitor.GCode.Commands.Standard
{
    public class Homing : Command
    {
        public Homing(string target) : base(CommandType.G, 28, new []{ target })
        {
        }
    }
}
