namespace Glitch.ApiModels
{
    public class TableApiModel
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Capacity { get; set; }
        public bool IsFree { get; set; }
        public int PlaceId { get; set; }
    }
}
