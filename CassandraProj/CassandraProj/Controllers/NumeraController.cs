using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; 
using Cassandra;  
using CassandraProj.Models;

namespace CassandraProj.Controllers
{
    public class NumeraController : Controller
    {
        ISession session = null;

        public NumeraController()
        {
            try
            {
                var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("OnlineMusicConcert");
            }
            catch (Exception ex)
            { }
        }

        public IActionResult PrikaziNumere()
        {
            NumeraData kk = new NumeraData();
            return View(kk);
        }

        public IActionResult prikaziNumereIzvodjaca(string idIzvodjaca)
        {
            List<Numera> l = new List<Numera>();
            try
            {
                var r = session.Execute("SELECT * FROM \"Numera\" WHERE " + "\"izvodjacID\" = " + "\'" + idIzvodjaca + "\' ; ");

                Numera n = null;
                foreach (var result in r)
                {
                    n = new Numera();
                    n.NumeraID = result.GetValue<string>("numeraID");
                    n.IzvodjacID = result.GetValue<string>("izvodjacID");
                    n.Ime = result.GetValue<string>("ime");
                    n.Trajanje = result.GetValue<string>("trajanje");
                    n.Autor = result.GetValue<string>("autor");

                    l.Add(n);
                }
            }
            catch (Exception ex)
            { }

            return View(l);
        }

        public IActionResult kreirajNumeru(string idIzvodjaca)
        {
            Numera kk = new Numera();
            kk.IzvodjacID = idIzvodjaca;

            return View(kk);
        }

        public IActionResult sacuvajNumeruUbazu(string IzvodjacID, string NumeraID, string Autor, string Ime, string Trajanje)
        {
            Numera k = null;
            var r = session.Execute("SELECT * FROM \"Numera\" WHERE " + "\"numeraID\" =\'" + NumeraID + "\' and \"izvodjacID\"=\'"+ IzvodjacID+"\'; ");

            foreach (var result in r)
            {
                k = new Numera();
                k.NumeraID = result.GetValue<string>("numeraID");
                k.IzvodjacID = result.GetValue<string>("izvodjacID");
                k.Ime = result.GetValue<string>("ime");
                k.Trajanje = result.GetValue<string>("trajanje");
                k.Autor = result.GetValue<string>("autor");
            }

            if (k != null)
                return RedirectToAction("kreirajNumeru", new { idIzvodjaca = IzvodjacID });

            Numera kk = new Numera();
            kk.IzvodjacID = IzvodjacID;
            kk.NumeraID = NumeraID;
            kk.Ime = Ime;
            kk.Autor = Autor;
            kk.Trajanje = Trajanje;

            if (proveriUneto(kk) == false)
                return RedirectToAction("kreirajNumeru", new { idIzvodjaca = IzvodjacID });

            try
            {
                session.Execute(" INSERT INTO \"Numera\"(\"izvodjacID\", \"numeraID\", autor, ime, trajanje)" +
                               "  VALUES(\'" + IzvodjacID + "\',\'" + kk.NumeraID + "\' ,\'" + kk.Autor + "\' ,\'" + kk.Ime + "\' ,\'" + kk.Trajanje + "\');");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("prikaziNumereIzvodjaca", new { idIzvodjaca = IzvodjacID });
        }

        public bool proveriUneto(Numera k)
        {
            if (k.IzvodjacID != null & k.NumeraID != null & k.Ime != null & k.Autor != null & k.Ime != null & k.Trajanje != null)
                return true;
            else 
                return false;
        }
    }
}
