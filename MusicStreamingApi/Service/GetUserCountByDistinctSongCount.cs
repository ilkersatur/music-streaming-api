using MusicStreamingApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayLogsService : IPlayLogsService
{
    public int GetUserCountByDistinctSongCount(DateTime date, int distinctSongCount)
    {
        var logs = CsvReaderHelper.ReadCsvFile();

        var filteredByDate = logs.Where(x => x.PlayTimeSpan.Date == date.Date);

        var userDistinctCounts = filteredByDate
            .GroupBy(x => x.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                DistinctCount = g.Select(x => x.SongId).Distinct().Count()
            });

        int count = userDistinctCounts.Count(u => u.DistinctCount == distinctSongCount);

        return count;
    }
}
