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
			string fileName = @"C:\Users\EC\Downloads\projekat\baza.txt";
			NetTcpBinding binding = new NetTcpBinding();
			string address = "net.tcp://localhost:9999/WCFService";

			ServiceHost host = new ServiceHost(typeof(WCFService));
			host.AddServiceEndpoint(typeof(IWCFService), binding, address);

            host.Open();
			Console.WriteLine("WCFService is opened.");
			Database db = new Database();
			//db.ToString();
			FileStream stream = null;
            try
            {
				stream = new FileStream(fileName, FileMode.OpenOrCreate);
				using(StreamWriter writer = new StreamWriter(fileName))
                {
					writer.WriteLine(db.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Console.WriteLine("WCFService is opened. Press <enter> to finish...");
            Console.ReadLine();

			host.Close();
		}
	}
}
