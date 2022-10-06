using System;
using System.Collections.Generic; 
using Cassandra; 

namespace CassandraProj.Models
{
    public class Bend
    {
        public string BendID { get; set; }
        public string Ime { get; set; }
        public string Brclanova { get; set; }
        public string Brojtel { get; set; }
    }

    public class BendData
    {
        public List<Bend> stdRecords = new List<Bend>();
        public BendData()
        {
            try
            {
                string konstr = "OnlineMusicConcert";
                using var cluster = Cluster.Builder()
                    .AddContactPoints("127.0.0.1").Build();
                var session = cluster.Connect(konstr);
                var results = session.Execute("select * from \"Bend\" ");
                Bend tObj = null;

                foreach (var result in results)
                {
                    tObj = new Bend();
                    tObj.BendID = result.GetValue<string>("bendID");
                    tObj.Ime = result.GetValue<string>("ime");
                    tObj.Brclanova = result.GetValue<string>("brclanova");
                    tObj.Brojtel = result.GetValue<string>("brojtel");

                    stdRecords.Add(tObj);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
