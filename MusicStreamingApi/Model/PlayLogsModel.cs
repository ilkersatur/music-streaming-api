
namespace MusicStreamingApi.Model
{
    public class PlayLogsModel
    {
        public string PlayId { get; set; }
        public uint SongId { get; set; }
        public uint ClientId { get; set; }
        public DateTime PlayTimeSpan { get; set; }

    }
}
