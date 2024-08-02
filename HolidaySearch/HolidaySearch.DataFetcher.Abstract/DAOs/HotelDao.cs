using Newtonsoft.Json;

namespace HolidaySearch.DataFetcher.Abstract.DAOs
{
    public class HotelDao
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("arrival_date")]
        public DateOnly ArrivalDate { get; set; }
        [JsonProperty("price_per_night")]
        public decimal PricePerNight { get; set; }
        [JsonProperty("local_airports")]
        public string[] LocalAirports { get; set; }
        public int Nights { get; set; }
    }
}
