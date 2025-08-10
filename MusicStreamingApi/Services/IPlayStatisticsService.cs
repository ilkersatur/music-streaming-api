using MusicStreamingApi.Models;

namespace MusicStreamingApi.Services
{
    public interface IPlayStatisticsService
    {
        IEnumerable<DistinctPlayDistribution> GetDistinctPlayDistribution(DateOnly targetDate);
    }
}
