using MusicStreamingApi.Model;

public interface IPlayLogsService
{
    IReadOnlyList<PlayLogsModel> GetPlayLogs();
    IReadOnlyList<PlayCountDistributionModel> GetDistinctPlayDistribution(DateTime date);
    public int GetUserCountByDistinctSongCount(DateTime date, int distinctSongCount);
    int GetMaxDistinctSongCount(DateTime date);
}