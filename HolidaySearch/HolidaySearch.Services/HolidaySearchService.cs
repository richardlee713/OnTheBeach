using HolidaySearch.DataFetcher.Abstract.DAOs;
using HolidaySearch.DataFetcher.Abstract.Interfaces;
using HolidaySearch.Services.Abstract.Criteria;
using HolidaySearch.Services.Abstract.DTOs;
using HolidaySearch.Services.Abstract.Interfaces;
using HolidaySearch.Services.Mappers;

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

            return HolidaySearchMapper.MapResults(flightsQuery, hotelQuery)
                .CalculatePrices()
                .OrderBy(r => r.TotalPrice);
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
    }
}
