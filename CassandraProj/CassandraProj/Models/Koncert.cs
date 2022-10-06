using System;
using System.Collections.Generic;
using Cassandra;

namespace CassandraProj.Models
{
    public class Koncert
    {
        public string KoncertID { get; set; }
        public string Ime { get; set; }
        public string Organizator { get; set; }
        public string Sponzor { get; set; }
        public string Opis { get; set; }
        public string Tip { get; set; }
    }

    public class KoncertData
    {
        public List<Koncert> stdRecords = new List<Koncert>();
        public KoncertData()
        {
            try
            {
                string konstr = "OnlineMusicConcert";
                using var cluster = Cluster.Builder()
                     .AddContactPoints("127.0.0.1").Build();
                var session = cluster.Connect(konstr);
                var results = session.Execute("select * from \"Koncert\" ");
                Koncert tObj = null;

                foreach (var result in results)
                {
                    tObj = new Koncert();
                    tObj.KoncertID = result.GetValue<string>("koncertID");
                    tObj.Ime = result.GetValue<string>("ime");
                    tObj.Organizator = result.GetValue<string>("organizator");
                    tObj.Sponzor = result.GetValue<string>("sponzor");
                    tObj.Opis = result.GetValue<string>("opis");
                    tObj.Tip = result.GetValue<string>("tip");

                    stdRecords.Add(tObj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
