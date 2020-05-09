using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrintingMonitor.Printer.Connection;
using PrintingMonitor.Printer.Models.Connection;

namespace PrintingMonitor.Pages.PrinterConnection.Connect
{
    public class ConnectPage : PageModel
    {
        private readonly IPrinterConnection _printerConnection;

        public ConnectPage(IPrinterConnection printerConnection)
        {
            _printerConnection = printerConnection;
        }

        [BindProperty]
        public ConnectParameters ConnectParameters { get; set; }

        public IReadOnlyCollection<SelectListItem> AvailableComPorts { get; private set; }

        public IReadOnlyCollection<SelectListItem> AvailableBaudRates { get; private set; }

        public IActionResult OnGet()
        {
            UpsertPortsInformation();
            return Page();
        }

        public IActionResult OnPost(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("/");

            if (ModelState.IsValid)
            {
                try
                {
                    _printerConnection.Connect(ConnectParameters);
                    return LocalRedirect(returnUrl);
                }
                catch (Exception exception)
                {

                    ModelState.AddModelError(string.Empty, exception.Message);
                    UpsertPortsInformation();
                    return Page();
                }
            }

            UpsertPortsInformation();
            return Page();
        }

        private void UpsertPortsInformation()
        {
            var availableConnectParameters = _printerConnection.GetConnectAvailableParameters();

            AvailableBaudRates = availableConnectParameters.BaudRates
                .Select(x => new SelectListItem(x.ToString(), x.ToString())).ToList();

            AvailableComPorts = availableConnectParameters.ComPorts
                .Select(x => new SelectListItem(x + " Port", x)).ToList();
        }
    }
}
