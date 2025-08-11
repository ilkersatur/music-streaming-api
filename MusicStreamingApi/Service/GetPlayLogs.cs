using MusicStreamingApi.Model;

public partial class PlayLogsService : IPlayLogsService
{
    public IReadOnlyList<PlayLogsModel> GetPlayLogs()
    {
        return CsvReaderHelper.ReadCsvFile();
    }
}
