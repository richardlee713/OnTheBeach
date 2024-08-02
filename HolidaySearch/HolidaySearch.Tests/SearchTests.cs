using HolidaySearch.DataFetcher.Abstract.Interfaces;
using HolidaySearch.Services.Abstract.Criteria;
using HolidaySearch.Services.Abstract.Interfaces;
using Xunit;

namespace HolidaySearch.Tests
{
    public class SearchTests
    {
        private readonly IHolidayDataFetcher _holidayDataFetcher;
        private readonly IHolidaySearchService _holidaySearchService;

        public SearchTests(IHolidayDataFetcher holidayDataFetcher,
            IHolidaySearchService holidaySearchService)
        {
            _holidayDataFetcher = holidayDataFetcher;
            _holidaySearchService = holidaySearchService;
        }

        [Fact]
        public void Can_Read_Flights_Source_File()
        {
            var flightData = _holidayDataFetcher.GetFlightData();
            Assert.True(flightData.Any());
        }

        [Fact]
        public void Can_Read_Hotels_Source_File()
        {
            var hotelData = _holidayDataFetcher.GetHotelData();
            Assert.True(hotelData.Any());
        }

        [Fact]
        public void Can_Run_Holiday_Search()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = "MAN",
                TravellingTo = "AGP",
                DepartureDate = new DateOnly(2023, 7, 1),
                DurationNights = 7
            };

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            Assert.True(searchResults.Any());
        }
    }
}
