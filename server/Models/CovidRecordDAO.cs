namespace server.Models;

public class CovidRecordDAO
{
    public String? Fips { get; set; }
    public String? Admin2 { get; set; }
    public String ProvinceState { get; set; }
    public String CountryRegion { get; set; }
    public DateTime LastUpdate { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Int64 Confirmed { get; set; }
    public Int64 Deaths { get; set; }
    public Int64 Recovered { get; set; }
    public Int64 Active { get; set; }
    public double IncidentRate { get; set; }
    public double CaseFatalityRatio { get; set; }


    public CovidRecordDAO(string countryRegion, DateTime lastUpdate, double latitude, double longitude, Int64 confirmed, Int64 deaths, Int64 recovered, Int64 active, double incidentRate, double caseFatalityRatio, string fips = "0", string admin2 = "0", string provinceState = " 0")
    {

        CountryRegion = countryRegion;
        LastUpdate = lastUpdate;
        Latitude = latitude;
        Longitude = longitude;
        Confirmed = confirmed;
        Deaths = deaths;
        Active = active;
        IncidentRate = incidentRate;
        CaseFatalityRatio = caseFatalityRatio;
        Fips = fips;
        Admin2 = admin2;
        ProvinceState = provinceState;
    }

}

