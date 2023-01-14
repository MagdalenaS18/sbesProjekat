using System;
using System.Collections.Generic;
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
			binding.Security.Mode = SecurityMode.Transport;
			binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

			X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);
			EndpointAddress address = new EndpointAddress(new Uri("net.tcp://localhost:9999/WCFService"),
									  new X509CertificateEndpointIdentity(srvCert));

			using (WCFClient proxy = new WCFClient(binding, address))
			{
				proxy.TestCommunication();
				Console.WriteLine("TestCommunication() finished. \n");

				Console.WriteLine("Unesite vasa sredstva: ");
				double sredstva = Double.Parse(Console.ReadLine());
				Korisnik korisnik = new Korisnik(WindowsIdentity.GetCurrent().Name, sredstva);
				korisnik.SredstvaNaRacunu = sredstva;
				korisnik.IdK = WindowsIdentity.GetCurrent().Name;
				//korisnik.IdK = WindowsIdentity.GetCurrent().Name;
				// provjeriti jel treba ovo iznad izbrisati idk i sredstva

				try
				{
					// POZIVANJE METODA
					// doda
					proxy.DodajKoncert(6, new Koncert(6, "Koncert", DateTime.Now.AddDays(5), "Grad", 100));
					// izmijeni
					proxy.IzmeniKoncert(6, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
					// ne izmijeni jer nema koncerta
					proxy.IzmeniKoncert(7, new Koncert(7, "Novi novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
					// napravi
					proxy.NapraviRezervaciju(new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
					// da li ce napraviti jer nema rezervacije s koncertom id=4?
					proxy.NapraviRezervaciju(new Rezervacija(1, 4, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
					// nece napraviti jer ne postoji koncert sa id=10
					proxy.NapraviRezervaciju(new Rezervacija(10, 10, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
					// nece napraviti jer je vec placena i vec postoji rezervacija sa id=2
					proxy.NapraviRezervaciju(new Rezervacija(2, 1, DateTime.Now.AddDays(1), 5, StanjeRezervacije.PLACENA));
					// platice
					proxy.PlatiRezervaciju(new Korisnik(WindowsIdentity.GetCurrent().Name, sredstva)
						, new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI)
						, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
					// nece platiti jer je vec placena
					proxy.PlatiRezervaciju(new Korisnik(WindowsIdentity.GetCurrent().Name, sredstva)
						, new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.PLACENA)
						, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));

				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					Console.WriteLine("[StackTrace] {0}", e.StackTrace);
				}
			}

			Console.ReadLine();
		}
	}
}
