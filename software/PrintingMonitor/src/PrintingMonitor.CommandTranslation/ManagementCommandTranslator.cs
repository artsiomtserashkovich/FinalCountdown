using System;
using System.Collections.Generic;
using System.Linq;
using PrintingMonitor.GCode.Commands;
using PrintingMonitor.GCode.Commands.Management.Printing;
using PrintingMonitor.GCode.Commands.Management.Temperature;
using PrintingMonitor.GCode.Commands.Standard;
using PrintingMonitor.Printer.Models.Commands.Management;

namespace PrintingMonitor.CommandTranslation
{
    internal class ManagementCommandTranslator
    {
        public IReadOnlyCollection<Command> Translate(ManagementCommand managementCommand)
        {
            switch (managementCommand)
            {
                case HomingCommand homingCommand:
                    return TranslateHomingCommand(homingCommand).ToList();

                case MoveCommand moveCommand:
                    return TranslateMoveCommand(moveCommand).ToList();

                case BedLevelingCommand _:
                    return new[] { new Homing("Z"),  };

                case SetFanSpeedCommand setFanSpeedCommand:
                    return new[] { new FanOn(setFanSpeedCommand.Speed),  };

                case SetTemperaturesCommand setTemperatureCommand:
                    return TranslateSetTemperaturesCommand(setTemperatureCommand).ToList();

                case StartPrintCommand startPrintCommand:
                    return TranslateStartPrintCommand(startPrintCommand).ToList();

                case StopPrintCommand _:
                    return new[] { new StopPrint() };

                default:
                    throw new InvalidOperationException("Unexpected type of user command was passed.");
            }
        }

        private static IEnumerable<Command> TranslateHomingCommand(HomingCommand homingCommand)
        {
            switch (homingCommand.Direction)
            {
                case HomingDirection.ALL:
                    yield return new Homing("X Y Z");
                    break;

                case HomingDirection.X:
                    yield return new Homing("X");
                    break;


                case HomingDirection.Y:
                    yield return new Homing("Y");
                    break;


                case HomingDirection.Z:
                    yield return new Homing("Z");
                    break;

                case HomingDirection.XY:
                    yield return new Homing("X Y");
                    break;
            }
        }

        private static IEnumerable<Command> TranslateMoveCommand(MoveCommand moveCommand)
        {
            yield return new SetToRelativePositioning();

            switch (moveCommand.MoveDirection)
            {
                case MoveDirection.Up:
                    yield return new LinearMove(moveCommand.Length,0,0,0);
                    break;

                case MoveDirection.Down:
                    yield return new LinearMove(-1 * moveCommand.Length, 0, 0, 0);
                    break;


                case MoveDirection.Left:
                    yield return new LinearMove(0, moveCommand.Length, 0, 0);
                    break;


                case MoveDirection.Right:
                    yield return new LinearMove(0, -1 * moveCommand.Length, 0, 0);
                    break;

                case MoveDirection.ExtruderUp:
                    yield return new LinearMove(0, 0, 0, moveCommand.Length);
                    break;


                case MoveDirection.ExtruderDown:
                    yield return new LinearMove(0, 0, 0, -1 * moveCommand.Length);
                    break;

                case MoveDirection.ZUp:
                    yield return new LinearMove(0, 0, moveCommand.Length, 0);
                    break;


                case MoveDirection.ZDown:
                    yield return new LinearMove(0, 0, -1 * moveCommand.Length, 0);
                    break;
            }

            yield return new SetToAbsolutePositioning();
        }

        private static IEnumerable<Command> TranslateSetTemperaturesCommand(SetTemperaturesCommand setTemperaturesCommand)
        {
            if (setTemperaturesCommand.Device == TemperatureDevice.Hotend)
            {
                yield return new SetHotendTemperature(setTemperaturesCommand.Temperature);
            }
            else
            { 
                yield return new SetBedTemperature(setTemperaturesCommand.Temperature);
            }
        }

        private static IEnumerable<Command> TranslateStartPrintCommand(StartPrintCommand startPrintCommand)
        {
            yield return new SelectFile(startPrintCommand.Filename);
            yield return new StartPrint();
        }

    }
}
