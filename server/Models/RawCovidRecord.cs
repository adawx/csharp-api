namespace server.Models;

public class RawRegionCovidRecord
{
    public String CountryRegion { get; set; }
    public DateTime LastUpdate { get; set; }
    public Int64 Confirmed { get; set; }
    public Int64 Deaths { get; set; }
    public Int64 Recovered { get; set; }
    public Int64 Active { get; set; }
    public double IncidentRate { get; set; }
    public double CaseFatalityRatio { get; set; }

    public RawRegionCovidRecord(string countryRegion, DateTime lastUpdate, Int64 confirmed, Int64 deaths, Int64 recovered, Int64 active, double incidentRate, double caseFatalityRatio)
    {
        CountryRegion = countryRegion;
        LastUpdate = lastUpdate;
        Confirmed = confirmed;
        Deaths = deaths;
        Active = active;
        IncidentRate = incidentRate;
        CaseFatalityRatio = caseFatalityRatio;
    }

}

