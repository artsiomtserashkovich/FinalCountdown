namespace PrintingMonitor.GCode.Commands.Management.Printing
{
    public class SelectFile : Command
    {
        public SelectFile(string filename) : base(CommandType.M, 23, new []{ filename }) { }
    }
}
