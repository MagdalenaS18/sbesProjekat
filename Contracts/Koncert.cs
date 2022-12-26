using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Contracts
{
    [DataContract]
    public class Koncert
    {

        int id;
        string naziv;
        DateTime vremePocetka;
        string lokacija;
        double cenaKarte;

        public Koncert(int id, string naziv, DateTime vremePocetka, string lokacija, double cenaKarte)
        {
            this.id = id;
            this.naziv = naziv;
            this.vremePocetka = vremePocetka;
            this.lokacija = lokacija;
            this.cenaKarte = cenaKarte;
        }
        [DataMember]
        public int Id { get => id; set => id = value; }

        [DataMember]
        public string Naziv { get => naziv; set => naziv = value; }

        [DataMember]
        public DateTime VremePocetka { get => vremePocetka; set => vremePocetka = value; }

        [DataMember]
        public string Lokacija { get => lokacija; set => lokacija = value; }

        [DataMember]
        public double CenaKarte { get => cenaKarte; set => cenaKarte = value; }

        public override string ToString()
        {
            return String.Format("Id : {0}, naziv : {1}, vreme pocetka : {2}, lokacija : {3} , cena karte : {4}", Id, Naziv, VremePocetka, Lokacija, CenaKarte);
        }
    }
}
