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
            var flightsQuery = _flightData
                .Where(f => f.To == criteria.TravellingTo &&
                    f.Date == criteria.DepartureDate)
                .AsQueryable();

            flightsQuery = ApplyDepartureAirportFilter(flightsQuery, criteria.DepartingFrom);

            var hotelQuery = _hotelData
                .Where(h => h.LocalAirports.Contains(criteria.TravellingTo) &&
                    h.Nights == criteria.DurationNights &&
                    h.ArrivalDate == criteria.DepartureDate)
                .AsQueryable();

            var searchResults = MapResults(criteria, flightsQuery, hotelQuery);

            return searchResults.OrderBy(r => r.TotalPrice);
        }

        private static IQueryable<FlightDao> ApplyDepartureAirportFilter(IQueryable<FlightDao> flightsQuery, DepartureAirportEnum from)
        {
            if (from == DepartureAirportEnum.Any) return flightsQuery;
            if (from == DepartureAirportEnum.AnyLondonAirport)
            {
                return flightsQuery.Where(f => f.From == "LGW" || f.From == "LTN");
            }
            else
            {
                return flightsQuery.Where(f => f.From == Enum.GetName(typeof(DepartureAirportEnum), from));
            }
        }

        private static List<HolidayResultDto> MapResults(HolidaySearchCriteria criteria, IEnumerable<FlightDao> matchingFlights, IEnumerable<HotelDao> matchingHotels)
        {
            var searchResults = new List<HolidayResultDto>();
            foreach (var flight in matchingFlights)
            {
                foreach (var hotel in matchingHotels)
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

            return searchResults;
        }
    }
}
