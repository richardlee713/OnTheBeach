using HolidaySearch.DataFetcher.Abstract.DAOs;
using HolidaySearch.Services.Abstract.DTOs;

namespace HolidaySearch.Services.Mappers
{
    public class HolidaySearchMapper
    {
        public static List<HolidayResultDto> MapResults(IEnumerable<FlightDao> matchingFlights, IEnumerable<HotelDao> matchingHotels)
        {
            var searchResults = new List<HolidayResultDto>();
            foreach (var flight in matchingFlights)
            {
                foreach (var hotel in matchingHotels)
                {
                    searchResults.Add(new HolidayResultDto
                    {
                        DepartingFrom = flight.From,
                        FlightId = flight.Id,
                        FlightPrice = flight.Price,
                        HotelId = hotel.Id,
                        HotelName = hotel.Name,
                        TravellingTo = flight.To,
                        HotelPricePerNight = hotel.PricePerNight,
                        Duration = hotel.Nights
                    });
                }
            }

            return searchResults;
        }
    }
}
