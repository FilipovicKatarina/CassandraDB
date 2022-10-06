using Microsoft.AspNetCore.Mvc;
using System; 
using Cassandra; 
using CassandraProj.Models;

namespace CassandraProj.Controllers
{
    public class IzvodjacController : Controller
    {
        ISession session = null;

        public IzvodjacController()
        {
            try
            {
                var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("OnlineMusicConcert");
            }
            catch (Exception ex)
            { }
        }

        public IActionResult PrikaziIzvodjace()
        {
            IzvodjacData kk = new IzvodjacData();

            return View(kk);
        }

        public IActionResult kreirajIzvodjace()
        {
            return View();
        }

        public IActionResult sacuvajIzvodjacaUbazu(string IzvodjacID, string Email, string Ime, string Menadzer, string Prezime, string Umetnickoime, string Zanr)
        {
            Izvodjac k = null;

            var r = session.Execute("SELECT * FROM \"Izvodjac\" WHERE " + "\"izvodjacID\" =\'" + IzvodjacID + "\'; ");
            foreach (var result in r)
            {
                k = new Izvodjac();

                k.IzvodjacID = result.GetValue<string>("izvodjacID");
                k.Ime = result.GetValue<string>("ime");
                k.Umetnickoime = result.GetValue<string>("umetnickoime");
                k.Prezime = result.GetValue<string>("prezime");
                k.Email = result.GetValue<string>("email");
                k.Zanr = result.GetValue<string>("zanr");
                k.Menadzer = result.GetValue<string>("menadzer");
            }

            if (k != null)
            {
                return RedirectToAction("kreirajIzvodjace");  
            }

            Izvodjac kk = new Izvodjac();
            kk.IzvodjacID = IzvodjacID;
            kk.Email = Email;
            kk.Ime = Ime;
            kk.Menadzer = Menadzer;
            kk.Prezime = Prezime;
            kk.Umetnickoime = Umetnickoime;
            kk.Zanr = Zanr;

            if (proveriUneto(kk) == false)
                return RedirectToAction("kreirajIzvodjace");

            try
            {
                session.Execute(" INSERT INTO \"Izvodjac\"(\"izvodjacID\", email, ime, menadzer, prezime, umetnickoime,zanr)" +
                               "  VALUES(\'" + IzvodjacID + "\',\'" + kk.Email + "\' ,\'" + kk.Ime + "\' ,\'" + kk.Menadzer + "\' ,\'" + kk.Prezime + "\' ,\'" + kk.Umetnickoime + "\', \'"+kk.Zanr+"\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziIzvodjace");
        }

        public bool proveriUneto(Izvodjac k)
        {
            if (k.IzvodjacID != null & k.Email != null & k.Ime != null & k.Menadzer != null & k.Prezime != null & k.Umetnickoime != null & k.Zanr != null)
                return true;
            else 
                return false;
        }

        public IActionResult ObrisiIzvodjaca(string izvodjacID)
        {
            try
            {
                session.Execute("DELETE FROM \"Izvodjac\" WHERE " + "\"izvodjacID\" =\'" + izvodjacID + "\'; ");
            }
            catch (Exception ex) { }

            return RedirectToAction("PrikaziIzvodjace");
        }

        public IActionResult PromeniIzvodjaca(string IzvodjacID)
        {
            return RedirectToAction("ModifikujIzvodjaca", new { izvodjacID = IzvodjacID });
        }

        public IActionResult SacuvajModifikovanogIzvodjaca(string IzvodjacID, string Ime, string Umetnickoime, string Prezime, string Email, string Zanr, string Menadzer)
        {
            Izvodjac ii = new Izvodjac();

            ii.IzvodjacID = IzvodjacID;
            ii.Ime = Ime;
            ii.Umetnickoime = Umetnickoime;
            ii.Prezime = Prezime;
            ii.Email = Email;
            ii.Zanr = Zanr;
            ii.Menadzer = Menadzer;

            if (proveriUneto(ii) == false)
                return RedirectToAction("ModifikujIzvodjaca", new { izvodjacID = IzvodjacID });

            try
            {
                session.Execute(" INSERT INTO \"Izvodjac\"(\"izvodjacID\", email, ime, menadzer, prezime, umetnickoime,zanr)" +
                               "  VALUES(\'" + IzvodjacID + "\',\'" + ii.Email + "\' ,\'" + ii.Ime + "\' ,\'" + ii.Menadzer + "\' ,\'" + ii.Prezime + "\' ,\'" + ii.Umetnickoime + "\', \'" + ii.Zanr + "\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziIzvodjace");
        }

        public IActionResult ModifikujIzvodjaca(string izvodjacID)
        {
            Izvodjac k = null;
            try
            {
                var r = session.Execute("SELECT * FROM \"Izvodjac\" WHERE " + "\"izvodjacID\" =\'" + izvodjacID + "\'; ");
                foreach (var result in r)
                {
                    k = new Izvodjac();
                    k.IzvodjacID = result.GetValue<string>("izvodjacID");
                    k.Ime = result.GetValue<string>("ime");
                    k.Umetnickoime = result.GetValue<string>("umetnickoime");
                    k.Prezime = result.GetValue<string>("prezime");
                    k.Email = result.GetValue<string>("email");
                    k.Zanr = result.GetValue<string>("zanr");
                    k.Menadzer = result.GetValue<string>("menadzer");
                }
            }
            catch (Exception ex) { }
 
            return View(k);
        }
    }
}
