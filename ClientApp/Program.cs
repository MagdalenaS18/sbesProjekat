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
			}

			Console.ReadLine();
		}
	}
}
