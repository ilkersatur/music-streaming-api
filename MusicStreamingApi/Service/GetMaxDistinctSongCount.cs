using MusicStreamingApi.Model;

public partial class PlayLogsService : IPlayLogsService
{
    public int GetMaxDistinctSongCount(DateTime date)
    {
        var logs = CsvReaderHelper.ReadCsvFile();

        var filteredByDate = logs.Where(x => x.PlayTimeSpan.Date == date.Date);

        var maxDistinct = filteredByDate
            .GroupBy(x => x.ClientId)
            .Select(g => g.Select(x => x.SongId).Distinct().Count())
            .DefaultIfEmpty(0)
            .Max();

        return maxDistinct;
    }
}
