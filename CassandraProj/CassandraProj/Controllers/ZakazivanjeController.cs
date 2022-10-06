using Microsoft.AspNetCore.Mvc;
using System;
using Cassandra; 
using CassandraProj.Models;

namespace CassandraProj.Controllers
{
    public class ZakazivanjeController : Controller
    {
        ISession session = null;

        public ZakazivanjeController()
        {
            try
            {
                var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("OnlineMusicConcert");
            }
            catch (Exception ex)
            { }
        }

        public IActionResult PrikaziZakazivanja()
        {
            ZakazivanjeData kk = new ZakazivanjeData();

            return View(kk);
        }

        public IActionResult kreirajZakazivanje()
        {
            return View();
        }

        public IActionResult sacuvajZakazivanjeUbazu(string zakID, string bendID, string datum, string dodatinf, string izvodjacID, string koncertID, string vreme)
        {
            Zakazivanje k= null;
            var r = session.Execute("SELECT * FROM \"Zakazivanje\" WHERE " + "\"zakID\" =\'" + zakID + "\'; ");

            foreach (var result in r)
            {
                k = new Zakazivanje();
                k.ZakID = result.GetValue<string>("zakID");
                k.IzvodjacID = result.GetValue<string>("izvodjacID");
                k.BendID = result.GetValue<string>("bendID");
                k.KoncertID = result.GetValue<string>("koncertID");
                k.Vreme = result.GetValue<string>("vreme");
                k.Datum = result.GetValue<string>("datum");
                k.Dodatinf = result.GetValue<string>("dodatinf");
            }

            if (k != null)
                return RedirectToAction("kreirajZakazivanje");

            Zakazivanje kk = new Zakazivanje();
            kk.ZakID = zakID;
            kk.BendID = bendID;
            kk.Datum = datum;
            kk.Dodatinf = dodatinf;
            kk.IzvodjacID = izvodjacID;
            kk.KoncertID = koncertID;
            kk.Vreme = vreme;

            if (proveriUneto(kk) == false)//proverava da li su sva polja uneta
                return RedirectToAction("kreirajZakazivanje");

            try
            {
                session.Execute(" INSERT INTO \"Zakazivanje\"(\"zakID\", \"bendID\", datum, dodatinf, \"izvodjacID\", \"koncertID\",vreme)" +
                               "  VALUES(\'" + zakID + "\',\'" + kk.BendID + "\' ,\'" + kk.Datum + "\' ,\'" + kk.Dodatinf + "\' ,\'" + kk.IzvodjacID + "\' ,\'" + kk.KoncertID + "\' ,\'" + kk.KoncertID + "\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziZakazivanja");

        }

        public bool proveriUneto(Zakazivanje k)
        {
            if (k.ZakID != null & k.BendID != null & k.Datum != null & k.Dodatinf != null & k.IzvodjacID != null & k.KoncertID != null & k.Vreme != null)
                return true;
            else
                return false;
        }

        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ObrisiZakazivanje(string zakID)
        {
            try
            {
                session.Execute("DELETE FROM \"Zakazivanje\" WHERE " + "\"zakID\" =\'" + zakID + "\'; ");
            }
            catch (Exception ex) { }

            return RedirectToAction("PrikaziZakazivanja");
        }

        public IActionResult PromeniZakazivanje(string zakID)
        {
            return RedirectToAction("ModifikujZakazivanje", new { zakID = zakID });
        }

        public IActionResult SacuvajModifikovanoZakazivanje(string ZakID, string IzvodjacID, string BendID, string KoncertID, string Vreme, string Datum,string Dodatinf)
        {
            Zakazivanje zz = new Zakazivanje();
            zz.ZakID = ZakID;
            zz.IzvodjacID = IzvodjacID;
            zz.BendID = BendID;
            zz.KoncertID = KoncertID;
            zz.Vreme = Vreme;
            zz.Datum = Datum;
            zz.Dodatinf = Dodatinf;

            if (proveriUneto(zz) == false)
                return RedirectToAction("ModifikujZakazivanje", new { zakID = ZakID });

            try
            {
                session.Execute(" INSERT INTO \"Zakazivanje\"(\"zakID\", \"bendID\", datum, dodatinf, \"izvodjacID\", \"koncertID\",vreme)" +
                              "  VALUES(\'" + ZakID + "\',\'" + zz.BendID + "\' ,\'" + zz.Datum + "\' ,\'" + zz.Dodatinf + "\' ,\'" + zz.IzvodjacID + "\' ,\'" + zz.KoncertID + "\' ,\'" + zz.KoncertID + "\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziZakazivanja");
        }

        public IActionResult ModifikujZakazivanje(string zakID)
        {
            Zakazivanje k = null;
            var r = session.Execute("SELECT * FROM \"Zakazivanje\" WHERE " + "\"zakID\" =\'" + zakID + "\'; ");

            foreach (var result in r)
            {
                k = new Zakazivanje();
                k.ZakID = result.GetValue<string>("zakID");
                k.IzvodjacID = result.GetValue<string>("izvodjacID");
                k.BendID = result.GetValue<string>("bendID");
                k.KoncertID = result.GetValue<string>("koncertID");
                k.Vreme = result.GetValue<string>("vreme");
                k.Datum = result.GetValue<string>("datum");
                k.Dodatinf = result.GetValue<string>("dodatinf");
            }

            return View(k);
        }
    }
}
