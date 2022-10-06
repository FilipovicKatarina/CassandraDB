using System;
using System.Collections.Generic;
using Cassandra; 

namespace CassandraProj.Models
{
    public class Numera
    {
        public string NumeraID { get; set; }
        public string IzvodjacID { get; set; }
        public string Ime { get; set; }
        public string Trajanje { get; set; }
        public string Autor { get; set; }
    }

    public class NumeraData
    {
        public List<Numera> stdRecords = new List<Numera>();
        public NumeraData()
        {
            try
            {
                string konstr = "OnlineMusicConcert";
                using var cluster = Cluster.Builder()
                     .AddContactPoints("127.0.0.1").Build();
                var session = cluster.Connect(konstr);
                var results = session.Execute("select * from \"Numera\" ");
                Numera tObj = null;

                foreach (var result in results)
                {
                    tObj = new Numera();
                    tObj.NumeraID = result.GetValue<string>("numeraID");
                    tObj.IzvodjacID = result.GetValue<string>("izvodjacID");
                    tObj.Ime = result.GetValue<string>("ime");
                    tObj.Trajanje = result.GetValue<string>("trajanje");
                    tObj.Autor = result.GetValue<string>("autor");

                    stdRecords.Add(tObj);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }

}
