using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using Contracts;

namespace ClientApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/WCFService";

			using (WCFClient proxy = new WCFClient(binding, new EndpointAddress(new Uri(address))))
			{
				Console.WriteLine("Unesite vasa sredstva: ");
				double sredstva = Double.Parse(Console.ReadLine());
				Korisnik korisnik = new Korisnik(WindowsIdentity.GetCurrent().Name, sredstva);
				korisnik.SredstvaNaRacunu = sredstva;
				korisnik.IdK = WindowsIdentity.GetCurrent().Name;
				// provjeriti jel treba ovo iznad izbrisati idk i sredstva

				// POZIVANJE METODA
				proxy.DodajKoncert(6, new Koncert(6, "Koncert", DateTime.Now.AddDays(5), "Grad", 100));
				proxy.IzmeniKoncert(6, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
				proxy.IzmeniKoncert(7, new Koncert(7, "Novi novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
				proxy.NapraviRezervaciju(new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
				proxy.NapraviRezervaciju(new Rezervacija(10, 10, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI));
				proxy.NapraviRezervaciju(new Rezervacija(2, 1, DateTime.Now.AddDays(1), 5, StanjeRezervacije.PLACENA));
				proxy.PlatiRezervaciju(new Korisnik(WindowsIdentity.GetCurrent().Name, sredstva)
					, new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.POTREBNO_PLATITI)
					, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
				proxy.PlatiRezervaciju(new Korisnik(WindowsIdentity.GetCurrent().Name, sredstva)
					, new Rezervacija(3, 2, DateTime.Now.AddDays(1), 5, StanjeRezervacije.PLACENA)
					, new Koncert(6, "Novi koncert", DateTime.Now.AddDays(5), "Grad", 100));
			}

			Console.ReadLine();
		}
	}
}
