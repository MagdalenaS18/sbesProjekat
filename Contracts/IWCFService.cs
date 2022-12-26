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
		[FaultContract(typeof(SecurityException))]
		bool DodajKoncert(int key, Koncert koncert);

		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		bool IzmeniKoncert(int key, Koncert koncert);


		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		bool NapraviRezervaciju(Rezervacija rezervacija);


		[OperationContract]
		[FaultContract(typeof(SecurityException))]
		bool PlatiRezervaciju(Korisnik korisnik, Rezervacija rezervacija, Koncert koncert);

	}
}
