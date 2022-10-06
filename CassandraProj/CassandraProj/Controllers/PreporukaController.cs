using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cassandra;
using CassandraProj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CassandraProj.Controllers
{
    public class PreporukaController : Controller
    {
        Cassandra.ISession session = null;
        
        public PreporukaController()
        {
            try
            {
                var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
                session = cluster.Connect("OnlineMusicConcert");
            }
            catch (Exception ex)
            { }
        }

        public IActionResult prikaziPreporukeKncerata()
        {
            List<Koncert> l = new List<Koncert>();
            var rr = session.Execute("SELECT * FROM \"preporuka\" ;");

            foreach (var result in rr)
            {
                var zanr = result.GetValue<string>("zanr");
                var r = session.Execute("SELECT * FROM \"Koncert\" WHERE tip =\'" + zanr + "\' ALLOW FILTERING;");

                foreach (var rs in r)
                {
                    Koncert k = new Koncert();
                    k.KoncertID = rs.GetValue<string>("koncertID");
                    k.Ime = rs.GetValue<string>("ime");
                    k.Organizator = rs.GetValue<string>("organizator");
                    k.Sponzor = rs.GetValue<string>("sponzor");
                    k.Opis = rs.GetValue<string>("opis");
                    k.Tip = rs.GetValue<string>("tip");
                    l.Add(k);
                }
            }

            return View(l);
        }

        // GET: Pretraga/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pretraga/Create
        public ActionResult Create()
        {
            return View();
        }
    }
}