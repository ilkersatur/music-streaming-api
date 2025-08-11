using MusicStreamingApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PlayLogsService : IPlayLogsService
{
    public IReadOnlyList<PlayCountDistributionModel> GetDistinctPlayDistribution(DateTime date)
    {
        var logs = CsvReaderHelper.ReadCsvFile();

        var filteredByDate = logs.Where(x => x.PlayTimeSpan.Date == date.Date);

        var clientDistinctCounts = filteredByDate
            .GroupBy(x => x.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                DistinctSongCount = g.Select(x => x.SongId).Distinct().Count()
            });

        var distribution = clientDistinctCounts
            .GroupBy(x => x.DistinctSongCount)
            .Select(g => new PlayCountDistributionModel
            {
                DistinctPlayCount = g.Key,
                ClientCount = g.Count()
            })
            .OrderBy(x => x.DistinctPlayCount)
            .ToList();

        return distribution;
    }
}
