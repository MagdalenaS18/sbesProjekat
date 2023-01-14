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
            //string grupa = SystemSecurityContext.Current.PrimaryIdentity.Name;
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            //Type x509IdentityType = identity.GetType();

            //FieldInfo certificateField = x509IdentityType.GetField("certificate", BindingFlags.Instance | BindingFlags.NonPublic);

            //X509Certificate2 certificate = (X509Certificate2)certificateField.GetValue(identity);

            //string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;    //NE RADI = NULL



            //string name = Formatter.ParseName(System.Security.Principal.WindowsIdentity.GetCurrent().Name);   NE RADI = NULL

            string clientName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            X509Certificate2 certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine,
                clientName);

            //X509Certificate2 certificate1 = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine,
            //    "wcfclient2");


            //string name = certificate.SubjectName.Name;  // cn=ooo,ou=ooo;840298
            //string[] clientName = name.Split(',');      // cn=0000   ou=ooo;098230894
            //string[] parts = clientName[1].Split(';');  // ou=ooo 098080
            //string[] roleName = parts[1].Split('=');    // ou oooo
            //string grupa = roleName[1];                     // ooo

            //string ime = certificate.SubjectName.Name.Split(',')[0].Split('=')[1];
            string grupa = certificate.SubjectName.Name.Split(',')[1].Split('=')[1];
            //string grupa1 = certificate1.SubjectName.Name.Split(',')[1].Split('=')[1];

            if (role.Equals(grupa))
            {
                return true;

                //if ((grupa.Equals("Admin") && ime.Equals("wcfclient2")) || (grupa.Equals("Korisnik") && ime.Equals("wcfclient")))
                //{

                //}
            }

            return false;

            //bool isInGroup = false;
            //bool check = false;

            //string group = "";
            //string client = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            //if (client.Contains(','))
            //{
            //    // kod njih je client.Split(',')[0].Split('=')[1]; ali nama je samo bitna grupa??
            //    group = client.Split(',')[1].Split('=')[1];
            //}

            //if (role.Equals(group))
            //{
            //    isInGroup = true;
            //}
            //else
            //{
            //    isInGroup = false;
            //}

            ////else if (group.Equals("Korisnik"))
            ////{
            ////    isInGroup = true;
            ////}

            //return isInGroup;
        }
    }
}
