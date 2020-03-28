using System.Security.Principal;

namespace PrintingMonitor.Data
{
    public class ApplicationUser : IIdentity
    {
        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; }

        public string Name { get; }

        public virtual string Password { get; set; }

        public string UserName { get; set; }
    }
}
