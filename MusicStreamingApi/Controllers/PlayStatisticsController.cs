using Microsoft.AspNetCore.Mvc;
using MusicStreamingApi.Models;
using MusicStreamingApi.Services;

namespace MusicStreamingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayStatisticsController : ControllerBase
    {
        private readonly IPlayStatisticsService _statisticsService;

        public PlayStatisticsController(IPlayStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("distinct-plays")]
        public ActionResult<IEnumerable<DistinctPlayDistribution>> GetDistinctPlayDistribution(
            [FromQuery] int year = 2016,
            [FromQuery] int month = 8,
            [FromQuery] int day = 10)
        {
            try
            {
                var date = new DateOnly(year, month, day);
                var result = _statisticsService.GetDistinctPlayDistribution(date);
                return Ok(result);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
