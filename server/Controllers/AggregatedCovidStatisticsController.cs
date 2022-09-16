using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repositories;

namespace server.Controllers
{

    [ApiController]
    [Route("api/covid/agg")]
    public class AggregatedCovidStatisticsController : ControllerBase
    {

        private readonly IConfiguration Configuration;
        private readonly ILogger<AggregatedCovidStatisticsController> _logger;

        private readonly CovidStatisticsRepository _covidRepo;

        public AggregatedCovidStatisticsController(IConfiguration configuration, ILogger<AggregatedCovidStatisticsController> logger)
        {
            Configuration = configuration;
            _logger = logger;
            _covidRepo = new CovidStatisticsRepository(Configuration);
        }

        [HttpGet(Name = "GetCovidStatisticsAggregate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AggregatedRegionCovidStats))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AggregatedRegionCovidStats>> Get([FromBody] RequestBody req)
        {

            // Validate input 
            if (string.IsNullOrEmpty(req.Region))
            {
                return BadRequest();
            }

            DateTime start;
            DateTime end;
            try
            {
                start = DateTime.Parse(req.StartDate);
                end = DateTime.Parse(req.EndDate);
                // Add a day to have search inclusive of selected date
                end.AddDays(1);
            } catch (Exception ex)
            {
                Console.WriteLine("Badly formatted date in request body");
                return BadRequest();

            }


            List<CovidRecordDAO> records = await _covidRepo.getRecordsByRegionAndDate(req.Region, start, end);

            //Build aggregated output object

            AggregatedRegionCovidStats result = new AggregatedRegionCovidStats(req.Region);

            if (records.Count == 0)
            {
                return NotFound();
            }

            foreach ( var record in records)
            {
                result.addConfirmed(record.Confirmed);
                result.addDeaths(record.Deaths);
                result.addRecovered(record.Recovered);
                result.addActive(record.Active);
            }

            return result;
        }
    }
}

