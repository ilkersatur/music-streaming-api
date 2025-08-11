using MusicStreamingApi.Model;
using System.Globalization;

public static class CsvReaderHelper
{
    public static List<PlayLogsModel> ReadCsvFile()
    {
        string filePath = "Data/exhibitA-input.csv";

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"CSV file not found at path: {filePath}");

        var result = new List<PlayLogsModel>();
        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines.Skip(1))
        {
            var columns = line.Split('\t');
            string dateStr = columns[3];
            DateTime playTime;

            if (!DateTime.TryParseExact(dateStr, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out playTime))
            {
                if (!DateTime.TryParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out playTime))
                {
                    continue;
                }
                else
                {
                }
            }

            result.Add(new PlayLogsModel
            {
                PlayId = columns[0],
                SongId = uint.Parse(columns[1]),
                ClientId = uint.Parse(columns[2]),
                PlayTimeSpan = playTime
            });
        }

        return result;
    }
}
