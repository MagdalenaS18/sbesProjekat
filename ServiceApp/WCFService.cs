using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Contracts;
using Manager;

namespace ServiceApp
{
    public class WCFService : IWCFService
    {
        //string fileName = @"C:\Users\wcfservice\Downloads\sbesSve\rjesenje (1)\rjesenje\novabaza.txt";
        string fileName = @"C:\Users\wcfservice\Downloads\sbesSve\rjesenje (1)\rjesenje\baza.txt";

        CustomPrincipal grupa = new CustomPrincipal(WindowsIdentity.GetCurrent());

        public void DodajUBazu(object obj)
        {

            //CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            //string userName = Formatter.ParseName(principal.Identity.Name);

            string userName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            if (grupa.IsInRole("Admin") || grupa.IsInRole("Korisnik"))

            //if (Thread.CurrentPrincipal.IsInRole("Admin") || principal.IsInRole("Korisnik"))
            {
                FileStream file = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                StreamWriter streamWriter;
                try
                {
                    using (streamWriter = new StreamWriter(file))
                    {
                        Console.WriteLine("\tFile Database is opened.\n");
                        Database db = new Database();
                        streamWriter.WriteLine("\n\t" + obj.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                try
                {
                    Audit.AddingToDatabaseSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                try
                {
                    Audit.AddingToDatabaseFailure(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "Adding to Database Failed.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call DodajUBazu method (time : {1}). " +
                    "For this method need to be member of group Admin/Korisnik.", userName, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));

                // VIDJETI DA LI OVO PREKIDA PROGRAM

                //throw new FaultException("User " + userName +
                //    " try to call Modify method. Modify method need  Modify permission.");
            }
        }

        public void DodajKoncert(int key, Koncert koncert)
        {
            string userName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            //CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            //string userName = Formatter.ParseName(principal.Identity.Name);

            if (grupa.IsInRole("Admin")) {

            //if (Thread.CurrentPrincipal.IsInRole("Admin")) {
                if (!Database.koncerti.ContainsKey(key))
                {
                    Database.koncerti.Add(key, koncert);
                    Console.WriteLine($"Koncert {koncert.Naziv} je uspjesno dodat.\n");

                    Console.WriteLine("\tAdmin is adding a Concert.\n");
                    DodajUBazu(koncert);
                }
                else
                {
                    Console.WriteLine("Dodavanje koncerta je neuspjesno, jer postoji vec takav koncert!\n");
                }

                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationFailure(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "DodajKoncert method need Admin permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call DodajKoncert method (time : {1}). " +
                    "For this method need to be member of group Admin.", userName, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }
        }

        public void IzmeniKoncert(int key, Koncert koncert)
        {
            string userName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            //CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            //string userName = Formatter.ParseName(principal.Identity.Name);

            if (grupa.IsInRole("Admin")) {

            //if (Thread.CurrentPrincipal.IsInRole("Admin")) {
                if (Database.koncerti.ContainsKey(key))
                {
                    Database.koncerti[key] = koncert;
                    Console.WriteLine($"Koncert {koncert.Naziv} je uspjesno izmijenjen.\n");

                    Console.WriteLine("\tAdmin is modifying a Concert.\n");
                    DodajUBazu(koncert);
                }
                else
                {
                    Console.WriteLine($"Nemoguce je izmijeniti koncert, jer ne postoji koncert sa ID: {key}!\n");
                }

                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationFailure(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "IzmeniKoncert method need Admin permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call IzmeniKoncert method (time : {1}). " +
                    "For this method need to be member of group Admin.", userName, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }
        }


        public void NapraviRezervaciju(Rezervacija rezervacija)
        {
            string userName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            //CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            //string userName = Formatter.ParseName(principal.Identity.Name);

            if (grupa.IsInRole("Korisnik"))

            //if (Thread.CurrentPrincipal.IsInRole("Korisnik"))
            {
                if (!Database.rezervacije.ContainsKey(rezervacija.IdR) && Database.rezervacije.ContainsKey(rezervacija.IdKoncerta))
                {
                    Rezervacija r = new Rezervacija(rezervacija.IdR, rezervacija.IdKoncerta, rezervacija.VremeRezervacije, rezervacija.KolicinaKarata, rezervacija.StanjeRezervacije = StanjeRezervacije.POTREBNO_PLATITI);
                    Database.rezervacije.Add(rezervacija.IdR, r);
                    Console.WriteLine($"Rezervacija je uspjesno napravljena!\n");

                    Console.WriteLine("\tKorisnik is adding a Reservation.\n");
                    DodajUBazu(rezervacija);
                }
                else
                {
                    if (!Database.koncerti.ContainsKey(rezervacija.IdKoncerta))
                    {
                        Console.WriteLine($"Nije moguce napraviti rezervaciju, jer ne postoji koncert sa ID: {rezervacija.IdKoncerta}\n");
                    }
                    else
                    {
                        Console.WriteLine($"Vec postoji rezervacija sa ID: {rezervacija.IdR}\n");
                    }
                }

                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationFailure(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "NapraviRezervaciju method need Korisnik permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call NapraviRezervaciju method (time : {1}). " +
                    "For this method need to be member of group Korisnik.", userName, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }

        }


        public void PlatiRezervaciju(Korisnik korisnik, Rezervacija rezervacija, Koncert koncert)
        {
            string userName = Formatter.ParseName(ServiceSecurityContext.Current.PrimaryIdentity.Name);

            //CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;
            //string userName = Formatter.ParseName(principal.Identity.Name);

            if (grupa.IsInRole("Korisnik"))
            
            //if(Thread.CurrentPrincipal.IsInRole("Korisnik"))
            {
                if (Database.koncerti.ContainsKey(koncert.Id) && Database.rezervacije.ContainsKey(rezervacija.IdR))
                {

                    if (korisnik.SredstvaNaRacunu >= (rezervacija.KolicinaKarata * koncert.CenaKarte) && rezervacija.StanjeRezervacije.ToString().Equals("POTREBNO_PLATITI"))
                    {
                        korisnik.SredstvaNaRacunu -= koncert.CenaKarte * rezervacija.KolicinaKarata;
                        rezervacija.StanjeRezervacije = StanjeRezervacije.PLACENA;

                        Console.WriteLine("Rezervacija je placena.\n");

                        Console.WriteLine("\tKorisnik is paying a Reservation.\n");
                        DodajUBazu(rezervacija);
                    }
                    else
                    {
                        if (!rezervacija.StanjeRezervacije.ToString().Equals("PLACENA"))
                        {
                            Console.WriteLine("Rezervacija nije placena, jer korisnik nema dovoljan iznos na racunu.\n");
                        }
                        else
                        {
                            Console.WriteLine("Rezervacija je vec placena!\n");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Doslo je do greske! Provjerite da li postoji uneseni koncert i rezervacija.\n");
                }

                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationSuccess(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                // LOGOVANJE ZA FUNKCIJU
                try
                {
                    Audit.AuthorizationFailure(userName,
                        OperationContext.Current.IncomingMessageHeaders.Action, "PlatiRezervaciju method need Korisnik permission.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                DateTime time = DateTime.Now;
                string message = String.Format("Access is denied. User {0} try to call PlatiRezervaciju method (time : {1}). " +
                    "For this method need to be member of group Korisnik.", userName, time.TimeOfDay);
                throw new FaultException<SecurityException>(new SecurityException(message));
            }

        }

        public void TestCommunication()
        {
            Console.WriteLine("Communication established.\n");
        }

    }
}
