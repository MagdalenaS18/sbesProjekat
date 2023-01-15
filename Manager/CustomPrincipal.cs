using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Manager
{
    public class CustomPrincipal : IPrincipal
    {
        WindowsIdentity identity = null;
        public CustomPrincipal(WindowsIdentity windowsIdentity)
        {
            identity = windowsIdentity;
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            foreach (IdentityReference group in this.identity.Groups)
            {
                //X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine,
                //Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

                //string grupa = certificate.SubjectName.Name.Split(',')[1].Split('=')[1];

                //if (role.Equals(grupa))
                //{
                //    return true;
                //}

                SecurityIdentifier sid = (SecurityIdentifier)group.Translate(typeof(SecurityIdentifier));
                var name = sid.Translate(typeof(NTAccount));
                string groupName = Formatter.ParseName(name.ToString());

                if (role.Equals(groupName))
                {
                    return true;
                }
            }
            return false;

        }
    }
}
