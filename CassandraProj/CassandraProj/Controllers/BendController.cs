using Microsoft.AspNetCore.Mvc;
using System; 
using Cassandra; 
using CassandraProj.Models;

namespace CassandraProj.Controllers
{
    public class BendController : Controller
    {
        ISession session = null;

        public BendController()
        {
            try
            {
                var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("OnlineMusicConcert");
            }
            catch (Exception ex)
            { }
        }

        public IActionResult PrikaziBendove()
        {
            BendData kk = new BendData();

            return View(kk);
        }

        public IActionResult kreirajBend(string BendID,string Brclanova,string Brojtel,string Ime)
        {
            return View();
        }

        public IActionResult sacuvajBenduBazi(string BendID, string Brclanova, string Brojtel, string Ime)
        {
            Bend k = null;

            var r = session.Execute("SELECT * FROM \"Bend\"  WHERE " + "\"bendID\" =" + "\'" + BendID + "\'" + " ; ");
            foreach (var result in r)
            {
                k = new Bend();

                k.BendID = result.GetValue<string>("bendID");
                k.Ime = result.GetValue<string>("ime");
                k.Brclanova = result.GetValue<string>("brclanova");
                k.Brojtel = result.GetValue<string>("brojtel");
            }

            if (k != null)
                return RedirectToAction("kreirajBend");

            Bend kk = new Bend();
            kk.BendID = BendID;
            kk.Brclanova = Brclanova;
            kk.Brojtel = Brojtel;
            kk.Ime = Ime;
           
            if (proveriUneto(kk) == false)
                return RedirectToAction("kreirajBend");

            try
            {
                session.Execute(" INSERT INTO \"Bend\"(\"bendID\", brclanova, brojtel, ime)" +
                               "  VALUES(\'" + BendID + "\',\'" + kk.Brclanova + "\' ,\'" + kk.Brojtel + "\' ,\'" + kk.Ime + "\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziBendove");
        }

        public bool proveriUneto(Bend k)
        {
            if (k.BendID != null & k.Brclanova != null & k.Brojtel != null & k.Ime != null )
                return true;
            else 
                return false;
        }

        public IActionResult ObrisiBend(string bendID)
        {
            try
            {
                session.Execute("DELETE FROM \"Bend\" WHERE " + "\"bendID\" =\'" + bendID + "\'; ");
            }
            catch (Exception ex) { }

            return RedirectToAction("PrikaziBendove");
        }

        public IActionResult PromeniBend(string bendID)
        {
            return RedirectToAction("ModifikujBend", new { bendID = bendID });
        }

        public IActionResult SacuvajModifikovanBend(string bendID, string Ime, string Brclanova, string Brojtel)
        {
            Bend bb = new Bend();
            bb.BendID = bendID;
            bb.Ime = Ime;
            bb.Brclanova = Brclanova;
            bb.Brojtel = Brojtel;

            if (proveriUneto(bb) == false)
                return RedirectToAction("ModifikujBend", new { bendID = bendID });

            try
            {
                session.Execute(" INSERT INTO \"Bend\"(\"bendID\", brclanova, brojtel, ime)" +
                                            "  VALUES(\'" + bendID + "\',\'" + bb.Brclanova + "\' ,\'" + bb.Brojtel + "\' ,\'" + bb.Ime + "\' );");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("PrikaziBendove");
        }

        public IActionResult ModifikujBend(string bendID)
        {
            Bend k = null;

            var r = session.Execute("SELECT * FROM \"Bend\"  WHERE " + "\"bendID\" =" + "\'" + bendID + "\'" + " ; ");
            foreach (var result in r)
            {
                k = new Bend();

                k.BendID = result.GetValue<string>("bendID");
                k.Ime = result.GetValue<string>("ime");
                k.Brclanova = result.GetValue<string>("brclanova");
                k.Brojtel = result.GetValue<string>("brojtel");
            }

            return View(k);
        }
    }
}
