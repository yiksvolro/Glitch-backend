namespace Glitch.ApiModels
{
    public class TableApiModel
    {
        public int Id { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Transform { get; set; }
        public int Capacity { get; set; }
        public bool IsFree { get; set; }
        public int PlaceId { get; set; }
    }
}
