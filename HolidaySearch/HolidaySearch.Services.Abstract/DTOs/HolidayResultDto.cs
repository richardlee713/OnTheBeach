namespace HolidaySearch.Services.Abstract.DTOs
{
    public class HolidayResultDto
    {
        public decimal TotalPrice { get; set; }
        public int FlightId { get; set; }
        public string DepartingFrom { get; set; }
        public string TravellingTo { get; set; }
        public decimal FlightPrice { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public decimal HotelPrice { get; set; }
        public decimal HotelPricePerNight { get; set; }
        public int Duration { get; set; }
    }
}
