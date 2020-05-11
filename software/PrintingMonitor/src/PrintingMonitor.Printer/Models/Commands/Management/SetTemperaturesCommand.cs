namespace PrintingMonitor.Printer.Models.Commands.Management
{
    public class SetTemperaturesCommand : ManagementCommand
    {
        public SetTemperaturesCommand(TemperatureDevice device, int temperature)
        {
            Device = device;
            Temperature = temperature;
        }

        public TemperatureDevice Device { get; }

        public int Temperature { get; }
    }

    public enum TemperatureDevice
    {
        Hotend,
        Bed
    }
}
