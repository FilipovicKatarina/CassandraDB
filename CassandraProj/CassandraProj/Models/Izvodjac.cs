using System;
using System.Collections.Generic; 
using Cassandra; 

namespace CassandraProj.Models
{
    public class Izvodjac
    {
        public string IzvodjacID { get; set; }
        public string Ime { get; set; }
        public string Umetnickoime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Zanr { get; set; }
        public string Menadzer { get; set; }
    }

    public class IzvodjacData
    {
        public List<Izvodjac> stdRecords = new List<Izvodjac>();
        public IzvodjacData()
        {
            try
            {
                string konstr = "OnlineMusicConcert";
                using var cluster = Cluster.Builder()
                    .AddContactPoints("127.0.0.1").Build();
                var session = cluster.Connect(konstr);
                var results = session.Execute("select * from \"Izvodjac\"");
                Izvodjac tObj = null;

                foreach (var result in results)
                {
                    tObj = new Izvodjac();
                    tObj.IzvodjacID = result.GetValue<string>("izvodjacID");
                    tObj.Ime = result.GetValue<string>("ime");
                    tObj.Umetnickoime = result.GetValue<string>("umetnickoime");
                    tObj.Prezime = result.GetValue<string>("prezime");
                    tObj.Email = result.GetValue<string>("email");
                    tObj.Zanr = result.GetValue<string>("zanr");
                    tObj.Menadzer = result.GetValue<string>("menadzer");

                    stdRecords.Add(tObj);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}
