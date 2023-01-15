using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contracts;
using Manager;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{

			string srvCertCN = "wcfservice";

			NetTcpBinding binding = new NetTcpBinding();
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

			//string client = Formatter.ParseName(SystemSecurityContext.Current.PrimaryIdentity.Name);

			X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, 
				StoreLocation.LocalMachine, srvCertCN);
			EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/WCFService"),
									  new X509CertificateEndpointIdentity(srvCert));

			//string address = "net.tcp://localhost:9999/WCFService";

			using (WCFClient proxy = new WCFClient(binding, address))
			{
				proxy.TestCommunication();
				Console.WriteLine("TestCommunication() finished. \n");

				Console.WriteLine("------- Korisnik koji je pokrenuo klijenta je: " + 
					Formatter.ParseName(WindowsIdentity.GetCurrent().Name) + "\n");

				string idKorisnika = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);

				Console.WriteLine("Unesite vasa sredstva: ");
				double sredstva = Double.Parse(Console.ReadLine());
				Korisnik korisnik = new Korisnik(idKorisnika, sredstva);
				korisnik.SredstvaNaRacunu = sredstva;
				korisnik.IdK = idKorisnika;
				// provjeriti jel treba ovo iznad izbrisati idk i sredstva

				proxy.DodajKoncert(6, new Koncert(6, "Koncert", DateTime.Now.AddDays(5), "Grad", 100));

				proxy.DodajKoncert(8, new Koncert(8, "K", DateTime.Now.AddDays(1), "Mjesto", 10));
				
				proxy.IzmeniKoncert(6, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
				
				proxy.IzmeniKoncert(7, new Koncert(7, "Novi novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
				
				proxy.NapraviRezervaciju(new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
				
				proxy.NapraviRezervaciju(new Rezervacija(1, 4, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
				
				proxy.NapraviRezervaciju(new Rezervacija(10, 10, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
				
				proxy.NapraviRezervaciju(new Rezervacija(2, 1, DateTime.Now.AddDays(1), 5, StanjeRezervacije.PLACENA));
				
				proxy.PlatiRezervaciju(new Korisnik(idKorisnika, sredstva)
					, new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI)
					, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
				
				proxy.PlatiRezervaciju(new Korisnik(idKorisnika, sredstva)
					, new Rezervacija(3, 4, DateTime.Now, 10, StanjeRezervacije.POTREBNO_PLATITI)
					, new Koncert(4, "Toni Cetinski", DateTime.Now.AddDays(36), "Trebinje", 80));

            }

			Console.ReadLine();
		}
	}
}
