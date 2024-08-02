using HolidaySearch.DataFetcher.Abstract.DAOs;

namespace HolidaySearch.DataFetcher.Abstract.Interfaces
{
    public interface IHolidayDataFetcher
    {
        IEnumerable<FlightDao> GetFlightData();
    }
}
