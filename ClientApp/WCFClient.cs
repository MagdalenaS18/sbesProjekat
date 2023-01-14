using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contracts;
using Manager;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;

namespace ClientApp
{
	public class WCFClient : ChannelFactory<IWCFService>, IWCFService, IDisposable
	{
		IWCFService factory;

        public WCFClient(NetTcpBinding binding, EndpointAddress address)
              : base(binding, address)
        {
            /// cltCertCN.SubjectName should be set to the client's username. .NET WindowsIdentity class provides information about Windows user running the given process
            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            /// Set appropriate client's certificate on the channel. Use CertManager class to obtain the certificate based on the "cltCertCN"
            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(StoreName.My, StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public void TestCommunication()
        {
            try
            {
                factory.TestCommunication();
            }
            catch (Exception e)
            {
                Console.WriteLine("[TestCommunication] ERROR = {0}", e.Message);
            }
        }

        public void Dispose()
		{
			if (factory != null)
			{
				factory = null;
			}

			this.Close();
		}

        public void DodajKoncert(int key, Koncert koncert)
        {
            try
            {
                factory.DodajKoncert(key, koncert);
                Console.WriteLine("Adding Concert allowed");
            }
            catch(FaultException<Contracts.SecurityException> se)
            {
                Console.WriteLine("Error while trying to access: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Adding a Concert : {0}", e.Message);
            }
        }

        public void IzmeniKoncert(int key, Koncert koncert)
        {
            try
            {
                factory.IzmeniKoncert(key, koncert);
                Console.WriteLine("Modify allowed");
            }
            catch (FaultException<SecurityException> se)
            {
                Console.WriteLine("Error while trying to access: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Modify : {0}", e.Message);
            }
        }

        public void NapraviRezervaciju(Rezervacija rezervacija)
        {
            try
            {
                factory.NapraviRezervaciju(rezervacija);
                Console.WriteLine("Adding Reservation allowed");
            }
            catch (FaultException<SecurityException> se)
            {
                Console.WriteLine("Error while trying to access: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Adding Reservation : {0}", e.Message);
            }
        }

        public void PlatiRezervaciju(Korisnik korisnik, Rezervacija rezervacija, Koncert koncert)
        {
            try
            {
                factory.PlatiRezervaciju(korisnik, rezervacija, koncert);
                Console.WriteLine("Paying Reservation allowed");
            }
            catch (FaultException<SecurityException> se)
            {
                Console.WriteLine("Error while trying to access: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Paying Reservation : {0}", e.Message);
            }
        }
    }
}
