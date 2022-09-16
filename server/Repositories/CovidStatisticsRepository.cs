using Microsoft.EntityFrameworkCore;
using Npgsql;
using server.Models;


namespace server.Repositories
{
    public interface ICovidStatisticsRepository
    {
        Task<List<CovidRecordDAO>> getRecordsByRegionAndDate(string searchRegion, DateTime startDate, DateTime endDate);
    }

    public class CovidStatisticsRepository: DbContext, ICovidStatisticsRepository
    {
        private readonly IConfiguration Configuration;
        private PostgreSqlRepository postgres;

        public CovidStatisticsRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            try
            {
                string connString = Configuration.GetSection("Config").GetValue<string>("Covid19DbConnectionString");
                postgres = new PostgreSqlRepository(
                    connString, "COVID_19");

            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CovidRecordDAO>> getRecordsByRegionAndDate(string searchRegion, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (NpgsqlConnection conn = postgres.GetConnection())
                {
                    await conn.OpenAsync();

                    string sql = String.Format("SELECT COALESCE(fips, '0') as fips,COALESCE(admin2, '0') as admin2,COALESCE(province_state, '0') as province_state,country_region,last_update,COALESCE(lat, 0) as lat,COALESCE(long_, 0) as long_,COALESCE(confirmed, 0) as confirmed,COALESCE(deaths, 0) as deaths,COALESCE(recovered, 0) as recovered,COALESCE(active, 0) as active,combined_key,COALESCE(incident_rate, 0) as incident_rate,COALESCE(case_fatality_ratio, 0) as case_fatality_ratio FROM records WHERE country_region='{0}' AND last_update BETWEEN '{1}' AND '{2}' LIMIT 10;", searchRegion, startDate, endDate);

                    await using var cmd = new NpgsqlCommand(sql, conn);

                    await using var reader = await cmd.ExecuteReaderAsync();

                    List<CovidRecordDAO> records = new List<CovidRecordDAO>();

                    while (await reader.ReadAsync())
                    {
                        string countryRegion = (string)reader["country_region"];
                        DateTime lastUpdate = (DateTime)reader["last_update"];
                        double latitude = (double)reader["lat"];
                        double longitude = (double)reader["long_"];
                        Int64 confirmed = (Int64)reader["confirmed"];
                        Int64 deaths = (Int64)reader["deaths"];
                        Int64 recovered = (Int64)reader["recovered"];
                        Int64 active = (Int64)reader["active"];
                        string combinedKey = (string)reader["combined_key"];
                        double incidentRate = (double)reader["incident_rate"];
                        double caseFatalityRatio = (double)reader["case_fatality_ratio"];
                        string? fips = reader["fips"] as string;
                        string? admin2 = reader["admin2"] as string;
                        string? provinceState = reader["province_state"] as string;

                        CovidRecordDAO covidRecord = new CovidRecordDAO(countryRegion, lastUpdate, latitude, longitude, confirmed, deaths, recovered, active, incidentRate, caseFatalityRatio, fips, admin2, provinceState);

                        records.Add(covidRecord);

                    }

                    return records;
                    
                }
            } catch( Exception ex)
            {
                Console.WriteLine("ERROR: Unable to get records");
                Console.WriteLine(ex.ToString());
                throw ex;
            }

        }
    }
}

