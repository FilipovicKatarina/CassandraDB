using Cassandra;
using CassandraProj.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CassandraProj.Controllers
{
    public class KoncertController : Controller
    {

        ISession session = null;
        int pr;

        public KoncertController()
        {
            try
            {
                 var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("OnlineMusicConcert");
            }
            catch (Exception ex)
            { }
        }

        public IActionResult PrikaziKoncerte()
        {
            KoncertData kk = new KoncertData();

            return View(kk);
        }

        public IActionResult kreirajKoncert()
        {
            return View();
        }

        public IActionResult sacuvajKoncertUbazu(string KoncertID, string Ime, string Organizator, string Opis, string Tip, string Sponzor)
        {
            Koncert k = null;
            var  r=session.Execute("SELECT * FROM \"Koncert\" WHERE " + "\"koncertID\" =\'"+KoncertID +"\'; ");

            foreach (var result in r)
            {
                k = new Koncert();
                k.KoncertID = result.GetValue<string>("koncertID");
                k.Ime = result.GetValue<string>("ime");
                k.Organizator = result.GetValue<string>("organizator");
                k.Sponzor = result.GetValue<string>("sponzor");
                k.Opis = result.GetValue<string>("opis");
                k.Tip = result.GetValue<string>("tip");
            }

            if(k !=null)
                return RedirectToAction("kreirajKoncert"); 

            Koncert kk = new Koncert();
            kk.KoncertID = KoncertID;
            kk.Ime = Ime;
            kk.Organizator = Organizator;
            kk.Sponzor = Sponzor;
            kk.Opis = Opis;
            kk.Tip = Tip;

            if (proveriUneto(kk) == false)
               return RedirectToAction("kreirajKoncert");

            try
            {
                session.Execute(" INSERT INTO \"Koncert\"(\"koncertID\", ime, opis,organizator,sponzor, tip)" +
                              "  VALUES(\'" + KoncertID + "\',\'" + kk.Ime + "\' ,\'" + kk.Opis + "\' ,\'" + kk.Organizator + "\' ,\'" + kk.Sponzor + "\' ,\'" + kk.Tip + "\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziKoncerte");
        }

        public bool proveriUneto(Koncert k)
        {
            if (k.KoncertID != null & k.Ime != null & k.Organizator != null & k.Opis != null & k.Tip != null & k.Sponzor != null)
                return true;
            else
                return false;
        }

        public IActionResult ObrisiKoncert(string koncertID)
        {
            try
            {
                session.Execute("DELETE FROM \"Koncert\" WHERE " + "\"koncertID\" =\'"+koncertID +"\'; ");
            }
            catch (Exception ex) { }

            return RedirectToAction("PrikaziKoncerte");
        }

        public IActionResult PromeniKoncert(string koncertID)
        {
            return RedirectToAction("ModifikujKoncert", new { koncertID = koncertID }) ; 
        }

        public IActionResult SacuvajModifikovanKoncert(string KoncertID,string Ime, string Organizator, string Opis, string Tip, string Sponzor)
        {
            Koncert kk = new Koncert();
            kk.KoncertID = KoncertID;
            kk.Ime = Ime;
            kk.Organizator = Organizator;
            kk.Sponzor = Sponzor;
            kk.Opis = Opis;
            kk.Tip = Tip;

            if (proveriUneto(kk) == false)
                return RedirectToAction("ModifikujKoncert", new { koncertID = KoncertID });

            try
            {
                session.Execute(" INSERT INTO \"Koncert\"(\"koncertID\", ime, opis,organizator,sponzor, tip)" +
                             "  VALUES(\'" + KoncertID + "\',\'" + kk.Ime + "\' ,\'" + kk.Opis + "\' ,\'" + kk.Organizator + "\' ,\'" + kk.Sponzor + "\' ,\'" + kk.Tip + "\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziKoncerte");
        }

        public IActionResult ModifikujKoncert(string koncertID)
        {
            Koncert k = null;
            var r = session.Execute("SELECT * FROM \"Koncert\" WHERE " + "\"koncertID\" =\'" + koncertID + "\'; ");

            foreach (var result in r)
            {
                k = new Koncert();
                k.KoncertID = result.GetValue<string>("koncertID");
                k.Ime = result.GetValue<string>("ime");
                k.Organizator = result.GetValue<string>("organizator");
                k.Sponzor = result.GetValue<string>("sponzor");
                k.Opis = result.GetValue<string>("opis");
                k.Tip = result.GetValue<string>("tip");
            }

            return View(k);
        }
        
        public IActionResult PretragaKoncerataPoTipu(string zanr)
        {
            List<Koncert> k = null; ;
            if (zanr != null)
            { 
                k = JsonConvert.DeserializeObject<List<Koncert>>(TempData["Lista"] as string);
            }

            return View(k);
        }

        public IActionResult prikaziRezPretrage(string zanr,string zanrC, string izvC)
        {
            List<Koncert> lista = new List<Koncert>();

            try
            {
                if (zanrC == "on")
                {
                    var r = session.Execute("SELECT * FROM \"Koncert\" WHERE tip =\'" + zanr + "\' ALLOW FILTERING;");
                    session.Execute(" INSERT INTO preporuka(zanr) VALUES(\'" + zanr+"\');");
                   
                    foreach (var result in r)
                    {
                        Koncert k = new Koncert();
                        k.KoncertID = result.GetValue<string>("koncertID");
                        k.Ime = result.GetValue<string>("ime");
                        k.Organizator = result.GetValue<string>("organizator");
                        k.Sponzor = result.GetValue<string>("sponzor");
                        k.Opis = result.GetValue<string>("opis");
                        k.Tip = result.GetValue<string>("tip");
                        lista.Add(k);
                    }
                }
                else if (izvC == "on")//prikazuje koncerte koji  imaju izvodjaca,tj veec su zakazani 
                {
                    var rr = session.Execute("SELECT \"koncertID\"  FROM \"Zakazivanje\" WHERE \"izvodjacID\" =\'" + zanr + "\' ALLOW FILTERING;");
                    
                    foreach (var result in rr)//stavka je id koncerta
                    {
                        var kid= result.GetValue<string>("koncertID");
                        var r = session.Execute("SELECT * FROM \"Koncert\" WHERE " + "\"koncertID\" =\'" + kid + "\'; ");

                        foreach (var rs in r)
                        {
                            Koncert k = new Koncert();
                            k.KoncertID = rs.GetValue<string>("koncertID");
                            k.Ime = rs.GetValue<string>("ime");
                            k.Organizator = rs.GetValue<string>("organizator");
                            k.Sponzor = rs.GetValue<string>("sponzor");
                            k.Opis = rs.GetValue<string>("opis");
                            k.Tip = rs.GetValue<string>("tip");
                            lista.Add(k);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            
            pr=1;
            TempData["Lista"] = JsonConvert.SerializeObject(lista);

            return RedirectToAction("PretragaKoncerataPoTipu", new { zanr = zanr });
        }
    }
}
