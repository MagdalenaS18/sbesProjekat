using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceApp
{
    public class Database
    {
        internal static Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();
        internal static Dictionary<int, Koncert> koncerti = new Dictionary<int, Koncert>();
        internal static Dictionary<int, Rezervacija> rezervacije = new Dictionary<int, Rezervacija>();

        static Database()
        {


            Koncert k1 = new Koncert(1, "Opera", DateTime.Now.AddYears(8), "Novi Sad", 23);
            Koncert k2 = new Koncert(2, "Zeljko Joksimovic", DateTime.Now.AddYears(3), "Banja Luka", 50);
            Koncert k3 = new Koncert(3, "2000's", DateTime.Now.AddMonths(6), "Beograd", 10);
            Koncert k4 = new Koncert(4, "Toni Cetinski", DateTime.Now.AddDays(36), "Trebinje", 80);
            Koncert k5 = new Koncert(1, "Vlado Georgijev", DateTime.Now.AddMonths(7), "Kragujevac", 35);

            Rezervacija r1 = new Rezervacija(2, 1, DateTime.Now, 22, StanjeRezervacije.PLACENA);
            Rezervacija r2 = new Rezervacija(4, 6, DateTime.Now, 12, StanjeRezervacije.POTREBNO_PLATITI);

            koncerti.Add(1, k1);
            koncerti.Add(2, k2);
            koncerti.Add(3, k3);
            koncerti.Add(4, k4);
            koncerti.Add(5, k5);

            rezervacije.Add(2, r1);
            rezervacije.Add(5, r2);

            Console.WriteLine("Spisak koncerata:");
            foreach (Koncert k in koncerti.Values)
            {
                //Console.WriteLine($"\n\tKoncert: {k.Id}, {k.Naziv}, {k.VremePocetka}, {k.Lokacija}, {k.CenaKarte}");
                string str = "";
                str += $"\n\tKoncert: {k.Id}, {k.Naziv}, {k.VremePocetka}, {k.Lokacija}, {k.CenaKarte}";
                //str += k.ToString();
                Console.WriteLine(str);
            }

            Console.WriteLine("Spisak rezervacija:");
            foreach (Rezervacija r in rezervacije.Values)
            {
                string str = "";
                str += $"\n\tRezervacije: {r.IdR}, {r.IdKoncerta}, {r.VremeRezervacije}, {r.StanjeRezervacije.ToString()}";
                Console.WriteLine(str);
            }

            //Console.WriteLine("Spisak korisnika:");
            //foreach (Korisnik k in korisnici.Values)
            //{
            //    string str = "";
            //    str += $"\n\tKorisnici: {k.IdK}, {k.SredstvaNaRacunu}";
            //    Console.WriteLine(str);
            //}

            //Console.WriteLine($"\n\tKoncert: {k1.Id}, {k1.Naziv}, {k1.VremePocetka}, {k1.Lokacija}, {k1.CenaKarte}");

            

            //Console.WriteLine("Spisak koncerata:");
            //foreach (Koncert k in koncerti.Values)
            //{
            //    koncerti.ToString();
            //}

            //Console.WriteLine("Spisak rezervacija:");
            //foreach (Rezervacija r in rezervacije.Values)
            //{
            //    rezervacije.ToString();
            //}

            //Console.WriteLine("Spisak korisnika:");
            //foreach (Korisnik k in korisnici.Values)
            //{
            //    korisnici.ToString();
            //}

        }

        //public override string ToString()
        //{
        //    Console.WriteLine("Spisak koncerata:");
        //    foreach (Koncert k in koncerti.Values)
        //    {
        //        koncerti.ToString();
        //    }

        //    Console.WriteLine("Spisak rezervacija:");
        //    foreach (Rezervacija r in rezervacije.Values)
        //    {
        //        rezervacije.ToString();
        //    }

        //    Console.WriteLine("Spisak korisnika:");
        //    foreach (Korisnik k in korisnici.Values)
        //    {
        //        korisnici.ToString();
        //    }

        //    return "";
        //}
    }
}
