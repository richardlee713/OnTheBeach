using HolidaySearch.DataFetcher.Abstract.DAOs;
using HolidaySearch.DataFetcher.Abstract.Interfaces;
using Newtonsoft.Json;

namespace HolidaySearch.DataFetcher
{
    public class HolidayDataFetcher : IHolidayDataFetcher
    {
        public IEnumerable<FlightDao> GetFlightData()
        {
            var flightData = JsonConvert.DeserializeObject<IEnumerable<FlightDao>>(File.ReadAllText("Data/flights.json"));
            return flightData ?? new List<FlightDao>();
        }

        public IEnumerable<HotelDao> GetHotelData()
        {
            var hotelData = JsonConvert.DeserializeObject<IEnumerable<HotelDao>>(File.ReadAllText("Data/hotels.json"));
            return hotelData ?? new List<HotelDao>();
        }
    }
}
