using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repositories;

namespace server.Controllers;

[ApiController]
[Route("api/covid/raw")]
public class RawCovidStatisticsController : ControllerBase
{

    private readonly IConfiguration Configuration;
    private readonly ILogger<RawCovidStatisticsController> _logger;

    private readonly CovidStatisticsRepository _covidRepo;

    public RawCovidStatisticsController(IConfiguration configuration, ILogger<RawCovidStatisticsController> logger)
    {
        Configuration = configuration;
        _logger = logger;
        _covidRepo = new CovidStatisticsRepository(Configuration);
    }

    [HttpGet(Name = "GetCovidStatisticsRaw")]
    public async Task<List<CovidRecordDAO>> Get([FromBody] RequestBody req)
    {

        DateTime start = DateTime.Parse(req.StartDate);
        DateTime end = DateTime.Parse(req.EndDate);
        // Add a day to have search inclusive of selected date
        end.AddDays(1);

        List<CovidRecordDAO> records = await _covidRepo.getRecordsByRegionAndDate(req.Region, start, end);

        return records;
    }
}
  