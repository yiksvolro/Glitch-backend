namespace Glitch.ApiModels
{
    public class PlaceApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string WorkTime { get; set; }
        public string Rating { get; set; }
        public int AllTables { get; set; }
        public int FreeTables { get; set; }
        public string PhoneNumber { get; set; }
        public string InstagramName { get; set; }
        public string Email { get; set; }
        public int UserId { get; set; }
    }
}
