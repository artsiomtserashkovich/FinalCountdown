using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrintingMonitor.Data;

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
        public ConnectModel Input { get; set; }

        public IEnumerable<SelectListItem> AvailableComPorts { get; private set; }

        public IEnumerable<SelectListItem> AvailableBaudRates { get; private set; }

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
                    _printerConnection.IsConnected = true;
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
            AvailableBaudRates = _printerConnection.AvailableBaudRate
                    .Select(x => new SelectListItem(x.ToString(), x.ToString()));

            AvailableComPorts = _printerConnection.AvailablePorts
                    .Select(x => new SelectListItem(x + " Port", x));
        }
    }
}
