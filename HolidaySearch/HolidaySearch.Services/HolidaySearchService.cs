using HolidaySearch.DataFetcher.Abstract.DAOs;
using HolidaySearch.DataFetcher.Abstract.Interfaces;
using HolidaySearch.Services.Abstract.Criteria;
using HolidaySearch.Services.Abstract.DTOs;
using HolidaySearch.Services.Abstract.Interfaces;
using System.Security.AccessControl;

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

            var hotelMatches = _hotelData
                .Where(h => h.LocalAirports.Contains(criteria.TravellingTo) &&
                    h.Nights == criteria.DurationNights &&
                    h.ArrivalDate == criteria.DepartureDate);

            var searchResults = MapResults(criteria, flightsQuery, hotelMatches);

            return searchResults.OrderBy(r => r.TotalPrice);
        }

        private static List<HolidayResultDto> MapResults(HolidaySearchCriteria criteria, IEnumerable<FlightDao> flightMatches, IEnumerable<HotelDao> hotelMatches)
        {
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

            return searchResults;
        }

        private static List<string> GetDepartureAirports(HolidaySearchCriteria criteria)
        {
            var departingFrom = new List<string>();

            switch (criteria.DepartingFrom)
            {
                case DepartureAirportEnum.AnyLondonAirport:
                    break;
                case DepartureAirportEnum.Any:
                    break;
                default:
                    break;
            }

            if (criteria.DepartingFrom == DepartureAirportEnum.AnyLondonAirport)
            {
                departingFrom.Add("LGW");
                departingFrom.Add("LTN");
            }
            else
            {
                departingFrom.Add(Enum.GetName(typeof(DepartureAirportEnum), criteria.DepartingFrom) ?? "");
            }

            return departingFrom;
        }

        private static IQueryable<FlightDao> ApplyDepartureAirportFilter(IQueryable<FlightDao> flightMatches, DepartureAirportEnum from)
        {
            if (from == DepartureAirportEnum.Any) return flightMatches;
            if (from == DepartureAirportEnum.AnyLondonAirport)
            {
                return flightMatches.Where(f => f.From == "LGW" || f.From == "LTN");
            }
            else
            {
                return flightMatches.Where(f => f.From == Enum.GetName(typeof(DepartureAirportEnum), from));
            }
        }
    }
}
