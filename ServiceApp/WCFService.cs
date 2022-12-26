using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Contracts;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        public bool DodajKoncert(int key, Koncert koncert)
        {
            if (!Database.koncerti.ContainsKey(key))
            {
                Database.koncerti.Add(key, koncert);
                return true;
            }

            else
            {
                // Console.WriteLine("Postoji vec takav koncert"); to  i logovati
                return false;
            }
        }

        public bool IzmeniKoncert(int key, Koncert koncert)
        {
            if (Database.koncerti.ContainsKey(key))
            {
                Database.koncerti[key] = koncert;
                return true;
            }
            else
            {
                // Console.WriteLine("Ne postoji takav koncert"); i logovati
                return false;
            }

        }

        public bool NapraviRezervaciju(Rezervacija rezervacija)
        {
            if (!Database.rezervacije.ContainsKey(rezervacija.IdR))
            {
                Rezervacija r = new Rezervacija(rezervacija.IdR, rezervacija.IdKoncerta, rezervacija.VremeRezervacije, rezervacija.KolicinaKarata, rezervacija.StanjeRezervacije = StanjeRezervacije.POTREBNO_PLATITI);
                Database.rezervacije.Add(rezervacija.IdR, r);
                return true;
            }
            else
            {
                //obavjestiti i logovati
                return false;
            }
        }


        public bool PlatiRezervaciju(Korisnik korisnik, Rezervacija rezervacija, Koncert koncert)
        {
            if (Database.koncerti.ContainsKey(koncert.Id) && Database.rezervacije.ContainsKey(rezervacija.IdR) && Database.korisnici.ContainsKey(korisnik.IdK))
            {

                if (korisnik.SredstvaNaRacunu >= rezervacija.KolicinaKarata * koncert.CenaKarte && rezervacija.StanjeRezervacije == StanjeRezervacije.POTREBNO_PLATITI)
                {
                    korisnik.SredstvaNaRacunu -= koncert.CenaKarte * rezervacija.KolicinaKarata;
                    rezervacija.StanjeRezervacije = StanjeRezervacije.PLACENA;

                    return true;
                    //logovati
                }
                else
                {
                    //obavjestiti  i logovati
                    return false;
                }
            }
            else
            {
                //obavjestiti i logovati
                return false;
            }
        }
    }
}
