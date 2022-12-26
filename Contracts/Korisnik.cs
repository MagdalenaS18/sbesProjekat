using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class Korisnik
    {
        //int idK;
        double sredstvaNaRacunu;

        //string idK = WindowsIdentity.GetCurrent().Name;
        string idK;

        public Korisnik(string idK, double sredstvaNaRacunu)
        {
            this.idK = idK;
            this.sredstvaNaRacunu = sredstvaNaRacunu;
        }

        [DataMember]
        public double SredstvaNaRacunu { get => sredstvaNaRacunu; set => sredstvaNaRacunu = value; }


        [DataMember]
        public string IdK { get => idK; set => idK = value; }

        public override string ToString()
        {
            return String.Format("Id korisnika: {0}, sredstva na racunu: {1}", IdK, sredstvaNaRacunu);
        }
    }
}
