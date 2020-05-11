using System;
using System.Collections.Generic;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.GCode.Commands.Management;
using PrintingMonitor.GCode.Commands.Management.Printing;
using PrintingMonitor.Printer.Models.Commands.Informations;

namespace PrintingMonitor.CommandTranslation
{
    internal class InformationCommandTranslator
    {
        public IReadOnlyCollection<Command> Translate(InformationCommand informationCommand)
        {
            switch (informationCommand)
            {
                case GetCurrentFirmwareInformation _:
                    return new[] { new ReadParamsFromEEPROM() };

                case GetPrintingFiles _:
                    return new[] { new ListSDCard() };

                default:
                    throw new InvalidOperationException("Unexpected type of user command was passed.");
            }
        }
    }
}
