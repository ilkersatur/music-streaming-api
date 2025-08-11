using Microsoft.AspNetCore.Mvc;
using MusicStreamingApi.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

[ApiController]
[Route("api/[controller]")]
public class PlayLogsController : ControllerBase
{
    private readonly IPlayLogsService _playLogsService;

    public PlayLogsController(IPlayLogsService playLogsService)
    {
        _playLogsService = playLogsService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<PlayLogsModel>> GetAll()
    {
        return Ok(_playLogsService.GetPlayLogs());
    }

    [HttpGet("distribution")]
    public ActionResult<IReadOnlyList<PlayCountDistributionModel>> GetDistribution(
        [FromQuery, DefaultValue(10)] int day = 10,
        [FromQuery, DefaultValue(8)] int month = 8,
        [FromQuery, DefaultValue(2016)] int year = 2016)
    {
        try
        {
            var date = new DateTime(year, month, day);
            var result = _playLogsService.GetDistinctPlayDistribution(date);
            return Ok(result);
        }
        catch (ArgumentOutOfRangeException)
        {
            return BadRequest("Invalid date parameters.");
        }
    }

    [HttpGet("usercount")]
    public ActionResult<int> GetUserCountByDistinctSongCount(
        [FromQuery, DefaultValue(346)] int distinctSongCount,
        [FromQuery, DefaultValue(10)] int day = 10,
        [FromQuery, DefaultValue(8)] int month = 8,
        [FromQuery, DefaultValue(2016)] int year = 2016)
    {
        if (distinctSongCount <= 0)
            return BadRequest("distinctSongCount must be greater than 0.");

        var date = new DateTime(year, month, day);
        int userCount = _playLogsService.GetUserCountByDistinctSongCount(date, distinctSongCount);

        return Ok(userCount);
    }


    [HttpGet("maxdistinctsongcount")]
    public ActionResult<int> GetMaxDistinctSongCount(
        [FromQuery, DefaultValue(10)] int day = 10,
        [FromQuery, DefaultValue(8)] int month = 8,
        [FromQuery, DefaultValue(2016)] int year = 2016)
    {
        var date = new DateTime(year, month, day);
        int maxCount = _playLogsService.GetMaxDistinctSongCount(date);
        return Ok(maxCount);
    }
}
