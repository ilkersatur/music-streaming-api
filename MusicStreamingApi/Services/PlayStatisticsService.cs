using MusicStreamingApi.Models;
using Microsoft.Extensions.Options;
using System.Globalization; // bunu eklemeyi unutma

namespace MusicStreamingApi.Services
{
    public class PlayStatisticsService : IPlayStatisticsService
    {
        private readonly FileSettings _fileSettings;
        private readonly CultureInfo _dateCulture = CultureInfo.GetCultureInfo("en-GB"); // dd/MM/yyyy için

        public PlayStatisticsService(IOptions<FileSettings> fileSettings)
        {
            _fileSettings = fileSettings.Value;
        }

        public IEnumerable<DistinctPlayDistribution> GetDistinctPlayDistribution(DateOnly targetDate)
        {
            string filePath = _fileSettings.InputFilePath;

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Input file not found: {filePath}");

            var clientSongMap = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);

            var formats = new[] { "dd/MM/yyyy HH:mm:ss", "dd/MM/yyyy" };

            foreach (var line in File.ReadLines(filePath).Skip(1)) // Skip header
            {
                var parts = line.Split('\t');
                if (parts.Length < 4) continue;

                var songId = parts[1];
                var clientId = parts[2];
                var playTsString = parts[3];

                if (!DateTime.TryParseExact(playTsString, formats, _dateCulture, DateTimeStyles.None, out var playDateTime))
                    continue;

                if (DateOnly.FromDateTime(playDateTime) != targetDate)
                    continue;

                if (!clientSongMap.TryGetValue(clientId, out var songs))
                {
                    songs = new HashSet<string>(StringComparer.Ordinal);
                    clientSongMap[clientId] = songs;
                }
                songs.Add(songId);
            }

            return clientSongMap
                .GroupBy(kvp => kvp.Value.Count)
                .Select(g => new DistinctPlayDistribution
                {
                    DistinctPlayCount = g.Key,
                    ClientCount = g.Count()
                })
                .OrderBy(r => r.DistinctPlayCount)
                .ToList();
        }
    }
}
