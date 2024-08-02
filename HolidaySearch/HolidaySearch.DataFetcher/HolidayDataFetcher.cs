using HolidaySearch.DataFetcher.Abstract.DAOs;
using HolidaySearch.DataFetcher.Abstract.Interfaces;

namespace HolidaySearch.DataFetcher
{
    public class HolidayDataFetcher : IHolidayDataFetcher
    {
        public IEnumerable<FlightDao> GetFlightData()
        {
            return new List<FlightDao>();
        }
    }
}
