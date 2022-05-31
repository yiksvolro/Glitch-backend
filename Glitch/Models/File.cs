using Glitch.Helpers.Enum;

namespace Glitch.Models
{
    public class File : IBaseModel
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public string FilePath { get; set; }
        public FileType Type { get; set; }
        public string Description { get; set; }
        public virtual Place Place { get; set; }
    }
}
