using HolidaySearch.DataFetcher.Abstract.DAOs;
using HolidaySearch.DataFetcher.Abstract.Interfaces;
using HolidaySearch.Services.Abstract.Criteria;
using HolidaySearch.Services.Abstract.DTOs;
using HolidaySearch.Services.Abstract.Interfaces;

namespace HolidaySearch.Services
{
    public class HolidaySearchService : IHolidaySearchService
    {
        private readonly IEnumerable<FlightDao> _flightData;
        private readonly IEnumerable<HotelDao> _hotelData;

        public HolidaySearchService(IHolidayDataFetcher holidayDataFetcher)
        {
            _flightData = holidayDataFetcher.GetFlightData();
            _hotelData = holidayDataFetcher.GetHotelData();
        }

        public IEnumerable<HolidayResultDto> HolidaySearch(HolidaySearchCriteria criteria)
        {
            var flightMatches = _flightData
                .Where(f => f.From == criteria.DepartingFrom &&
                    f.To == criteria.TravellingTo &&
                    f.Date == criteria.DepartureDate);

            var hotelMatches = _hotelData
                .Where(h => h.LocalAirports.Contains(criteria.TravellingTo) &&
                    h.Nights == criteria.DurationNights &&
                    h.ArrivalDate == criteria.DepartureDate);

            var searchResults = new List<HolidayResultDto>();
            foreach (var flight in flightMatches)
            {
                foreach (var hotel in hotelMatches)
                {
                    var hotelPrice = hotel.PricePerNight * criteria.DurationNights;

                    searchResults.Add(new HolidayResultDto
                    {
                        DepartingFrom = flight.From,
                        FlightId = flight.Id,
                        FlightPrice = flight.Price,
                        HotelId = hotel.Id,
                        HotelName = hotel.Name,
                        HotelPrice = hotelPrice,
                        TravellingTo = flight.To,
                        HotelPricePerNight = hotel.PricePerNight,
                        TotalPrice = hotelPrice + flight.Price
                    });
                }
            }

            return searchResults.OrderBy(r => r.TotalPrice);
        }
    }
}
