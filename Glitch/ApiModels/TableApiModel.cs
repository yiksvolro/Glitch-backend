namespace Glitch.ApiModels
{
    public class TableApiModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Top { get; set; }
        public string Left { get; set; }
        public string Transform { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }
        public bool IsFree { get; set; }
        public int PlaceId { get; set; }
    }
}
