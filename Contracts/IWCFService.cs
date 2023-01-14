using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Contracts
{
	[ServiceContract]
	public interface IWCFService
	{
		[OperationContract]
		void TestCommunication();

		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		void DodajKoncert(int key, Koncert koncert);

		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		void IzmeniKoncert(int key, Koncert koncert);


		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		void NapraviRezervaciju(Rezervacija rezervacija);


		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		void PlatiRezervaciju(Korisnik korisnik, Rezervacija rezervacija, Koncert koncert);

		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		void DodajUBazu(object obj);
	}
}
