using System;
namespace server.Models
{
    public class AggregatedRegionCovidStats
    {

        public string Region { get; set; }
        public Int64 TotalConfirmed { get; set; }
        public Int64 TotalDeaths { get; set; }
        public Int64 TotalRecovered { get; set; }
        public Int64 TotalActive { get; set; }


        public AggregatedRegionCovidStats(string region)
        {
            Region = region;
            TotalConfirmed = 0;
            TotalDeaths = 0;
            TotalRecovered = 0;
            TotalActive = 0;
        }

        public void addConfirmed(Int64 add)
        {
            TotalConfirmed += add;
        }

        public void addDeaths(Int64 add)
        {
            TotalDeaths += add;
        }

        public void addRecovered(Int64 add)
        {
            TotalRecovered += add;
        }

        public void addActive(Int64 add)
        {
            TotalActive += add;
        }
    }
}

