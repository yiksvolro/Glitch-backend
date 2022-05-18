using System;


public class BookingApiModel
{
	public BookingApiModel()
	{
        public string Id { get; set; }
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public int TableId { get; set; }
        public DateTime BookedOn { get; set; }
    }
}
