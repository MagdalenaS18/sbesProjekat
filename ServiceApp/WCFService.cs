using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contracts;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        
        public void DodajKoncert(int key, Koncert koncert)
        {
            //if (Thread.CurrentPrincipal.IsInRole("Admin"))
            //{
            //    if (!Database.koncerti.ContainsKey(key))
            //    {
            //        Database.koncerti.Add(key, koncert);
            //        Console.WriteLine($"Koncert {koncert.Naziv} je uspjesno dodat.\n");
            //        return true;
            //    }else
            //    {
            //        Console.WriteLine("Dodavanje koncerta je neuspjesno, jer postoji vec takav koncert!\n"); //to  i logovati
            //        return false;
            //    }
            //}
            //else
            //{
            //    Contracts.SecurityException se = new Contracts.SecurityException();
            //    se.Message = string.Format($"User {Thread.CurrentPrincipal.Identity.Name} tried to call DodajKoncert method. User needs to be: Admin role {DateTime.Now}");
            //    throw new FaultException<Contracts.SecurityException>(se);
            //}
            
        }

        
        public void IzmeniKoncert(int key, Koncert koncert)
        {
            //if (Thread.CurrentPrincipal.IsInRole("Admin"))
            //{
            //    if (Database.koncerti.ContainsKey(key))
            //    {
            //        Database.koncerti[key] = koncert;
            //        Console.WriteLine($"Koncert {koncert.Naziv} je uspjesno izmijenjen.\n");
            //        return true;
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Nemoguce je izmijeniti koncert, jer ne postoji koncert sa ID: {key}!\n"); //i logovati
            //        return false;
            //    }
            //}
            //else
            //{
            //    Contracts.SecurityException se = new Contracts.SecurityException();
            //    se.Message = string.Format($"User {Thread.CurrentPrincipal.Identity.Name} tried to call IzmeniKoncert method. User needs to be: Admin role {DateTime.Now}");
            //    throw new FaultException<Contracts.SecurityException>(se);
            //}
        }

        
        public void NapraviRezervaciju(Rezervacija rezervacija)
        {
            //if (Thread.CurrentPrincipal.IsInRole("User"))
            //{
            //    if (!Database.rezervacije.ContainsKey(rezervacija.IdR) && Database.rezervacije.ContainsKey(rezervacija.IdKoncerta))
            //    {
            //        Rezervacija r = new Rezervacija(rezervacija.IdR, rezervacija.IdKoncerta, rezervacija.VremeRezervacije, rezervacija.KolicinaKarata, rezervacija.StanjeRezervacije = StanjeRezervacije.POTREBNO_PLATITI);
            //        Database.rezervacije.Add(rezervacija.IdR, r);
            //        Console.WriteLine($"Rezervacija je uspjesno napravljena!\n");
            //        return true;
            //    }
            //    else
            //    {
            //        if (!Database.koncerti.ContainsKey(rezervacija.IdKoncerta))
            //        {
            //            Console.WriteLine($"Nije moguce napraviti rezervaciju, jer ne postoji koncert sa ID: {rezervacija.IdKoncerta}\n");
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Vec postoji rezervacija sa ID: {rezervacija.IdR}\n"); //obavjestiti i logovati
            //        }

                
            //        return false;
            //    }
            //}
            //else
            //{
            //    Contracts.SecurityException se = new Contracts.SecurityException();
            //    se.Message = string.Format($"User {Thread.CurrentPrincipal.Identity.Name} tried to call NapraviRezervaciju method. User needs to be: User role {DateTime.Now}");
            //    throw new FaultException<Contracts.SecurityException>(se);
            //}
        }

        
        public void PlatiRezervaciju(Korisnik korisnik, Rezervacija rezervacija, Koncert koncert)
        {
        //    if (Thread.CurrentPrincipal.IsInRole("User"))
        //    {
        //        if (Database.koncerti.ContainsKey(koncert.Id) && Database.rezervacije.ContainsKey(rezervacija.IdR))
        //        {

        //            if (korisnik.SredstvaNaRacunu >= (rezervacija.KolicinaKarata * koncert.CenaKarte) && rezervacija.StanjeRezervacije.ToString().Equals("POTREBNO_PLATITI"))
        //            {
        //                korisnik.SredstvaNaRacunu -= koncert.CenaKarte * rezervacija.KolicinaKarata;
        //                rezervacija.StanjeRezervacije = StanjeRezervacije.PLACENA;

        //                Console.WriteLine("Rezervacija je placena.\n");

        //                return true;
        //                //logovati
        //            }
        //            else
        //            {
        //                if (!rezervacija.StanjeRezervacije.ToString().Equals("PLACENA"))
        //                {
        //                    Console.WriteLine("Rezervacija nije placena, jer korisnik nema dovoljan iznos na racunu.\n");
        //                }

        //                Console.WriteLine("Rezervacija je vec placena!\n");

        //                //obavjestiti  i logovati
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Doslo je do greske! Provjerite da li postoji uneseni koncert i rezervacija.\n");
        //            //obavjestiti i logovati
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        Contracts.SecurityException se = new Contracts.SecurityException();
        //        se.Message = string.Format($"User {Thread.CurrentPrincipal.Identity.Name} tried to call PlatiRezervaciju method. User needs to be: User role {DateTime.Now}");
        //        throw new FaultException<Contracts.SecurityException>(se);
        //    }
        }

        public void TestCommunication()
        {
            Console.WriteLine("Communication established.");
        }
    }
}
