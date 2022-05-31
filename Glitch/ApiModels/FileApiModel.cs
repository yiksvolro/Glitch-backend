using Glitch.Helpers.Enum;

namespace Glitch.ApiModels
{
    public class FileApiModel
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public FileType Type { get; set; }
    }
}
