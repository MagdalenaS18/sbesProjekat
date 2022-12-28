using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using Contracts;
using System.IO;
using static System.Net.WebRequestMethods;

namespace ServiceApp
{
	public class Program
	{
		static void Main(string[] args)
		{
			string fileName = @"C:\Users\HP\Desktop\sbesProjekat\sbesProjekat\baza.txt";
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/WCFService";

			ServiceHost host = new ServiceHost(typeof(WCFService));
			host.AddServiceEndpoint(typeof(IWCFService), binding, address);

            host.Open();
			Console.WriteLine("WCFService is opened.");
			Database db = new Database();
			//db.ToString();
			FileStream file = new FileStream(fileName, FileMode.OpenOrCreate);
			Console.WriteLine("File opened");
			StreamWriter streamWriter = null;
			try
			{
				using (streamWriter = new StreamWriter(file))
				{
					streamWriter.WriteLine("Spisak koncerata:");
					foreach (Koncert k in Database.koncerti.Values)
					{
						k.ToString();
						streamWriter.WriteLine("\n\t" + k);
					}
					streamWriter.WriteLine("Spisak rezervacija:");
					foreach (Rezervacija r in Database.rezervacije.Values)
					{
						r.ToString();
						streamWriter.WriteLine("\n\t" + r);

					}
				}
			} catch(Exception e)
            {
				Console.WriteLine(e.Message);
			}
			//streamWriter.Close();
			//file.Close();
            //Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();

			host.Close();
		}
	}
}
