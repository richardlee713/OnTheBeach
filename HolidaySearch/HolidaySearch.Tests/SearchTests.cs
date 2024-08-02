using HolidaySearch.DataFetcher.Abstract.Interfaces;
using Xunit;

namespace HolidaySearch.Tests
{
    public class SearchTests
    {
        private readonly IHolidayDataFetcher _holidayDataFetcher;

        public SearchTests(IHolidayDataFetcher holidayDataFetcher)
        {
            _holidayDataFetcher = holidayDataFetcher;
        }

        [Fact]
        public void Can_Read_Source_Files()
        {
            var flightData = _holidayDataFetcher.GetFlightData();
            Assert.True(flightData.Any());
        }
    }
}
