using System;
using System.Collections.Generic;
using Cassandra;

namespace CassandraProj.Models
{
    public class Zakazivanje
    {
        public string ZakID { get; set; }
        public string IzvodjacID { get; set; }
        public string BendID { get; set; }
        public string KoncertID { get; set; }
        public string Vreme { get; set; }
        public string Datum { get; set; }
        public string Dodatinf { get; set; }
    }

    public class ZakazivanjeData
    {
        public List<Zakazivanje> stdRecords = new List<Zakazivanje>();
        public ZakazivanjeData()
        {
            try
            {
                string konstr = "OnlineMusicConcert";
                using var cluster = Cluster.Builder()
                    .AddContactPoints("127.0.0.1").Build();
                var session = cluster.Connect(konstr);
                var results = session.Execute("select * from \"Zakazivanje\" ");
                Zakazivanje tObj = null;

                foreach (var result in results)
                {
                    tObj = new Zakazivanje();
                    tObj.ZakID = result.GetValue<string>("zakID");
                    tObj.IzvodjacID = result.GetValue<string>("izvodjacID");
                    tObj.BendID = result.GetValue<string>("bendID");
                    tObj.KoncertID = result.GetValue<string>("koncertID");
                    tObj.Vreme = result.GetValue<string>("vreme");
                    tObj.Datum = result.GetValue<string>("datum");
                    tObj.Dodatinf = result.GetValue<string>("dodatinf");

                    stdRecords.Add(tObj);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }

}
