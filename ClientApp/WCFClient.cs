using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Contracts;

namespace ClientApp
{
	public class WCFClient : ChannelFactory<IWCFService>, IWCFService, IDisposable
	{
		IWCFService factory;

		public WCFClient(NetTcpBinding binding, EndpointAddress address)
			: base(binding, address)
		{
			factory = this.CreateChannel();
		}

        public void Dispose()
		{
			if (factory != null)
			{
				factory = null;
			}

			this.Close();
		}

        public bool DodajKoncert(int key, Koncert koncert)
        {
            //throw new NotImplementedException();

            //Koncert kon = null;

            bool retValue = false;
            try
            {
                retValue = factory.DodajKoncert(key, koncert);
                Console.WriteLine("Adding Concert allowed");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Adding a Concert : {0}", e.Message);
            }
            return false;
        }

        public bool IzmeniKoncert(int key, Koncert koncert)
        {
            //throw new NotImplementedException();

            bool retValue = false;
            try
            {
                retValue = factory.IzmeniKoncert(key, koncert);
                Console.WriteLine("Modify allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Modify : {0}", e.Message);
            }
            return retValue;
        }

        public bool NapraviRezervaciju(Rezervacija rezervacija)
        {
            //throw new NotImplementedException();

            bool retValue = false;
            try
            {
                retValue = factory.NapraviRezervaciju(rezervacija);
                Console.WriteLine("Adding Reservation allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Adding Reservation : {0}", e.Message);
            }
            return retValue;
        }

        public bool PlatiRezervaciju(Korisnik korisnik, Rezervacija rezervacija, Koncert koncert)
        {
            //throw new NotImplementedException();

            bool retValue = false;
            try
            {
                retValue = factory.PlatiRezervaciju(korisnik, rezervacija, koncert);
                Console.WriteLine("Paying Reservation allowed");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to Paying Reservation : {0}", e.Message);
            }
            return retValue;
        }
    }
}
