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
        public void Verify_Holiday_Search_Customer_1()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = DepartureAirportEnum.MAN,
                TravellingTo = "AGP",
                DepartureDate = new DateOnly(2023, 7, 1),
                DurationNights = 7
            };

            const int expectedFlightId = 2;
            const int expectedHotelId = 9;
            const decimal expectedTotalPrice = 826;

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            var bestValueResult = searchResults.FirstOrDefault();

            Assert.NotNull(bestValueResult);
            Assert.Equal(expectedFlightId, bestValueResult.FlightId);
            Assert.Equal(expectedHotelId, bestValueResult.HotelId);
            Assert.Equal(expectedTotalPrice, bestValueResult.TotalPrice);
        }
        
        [Fact]
        public void Verify_Holiday_Search_Customer_2()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = DepartureAirportEnum.AnyLondonAirport,
                TravellingTo = "PMI",
                DepartureDate = new DateOnly(2023, 6, 15),
                DurationNights = 10
            };

            const int expectedFlightId = 6;
            const int expectedHotelId = 5;
            const decimal expectedTotalPrice = 675;

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            var bestValueResult = searchResults.FirstOrDefault();

            Assert.NotNull(bestValueResult);
            Assert.Equal(expectedFlightId, bestValueResult.FlightId);
            Assert.Equal(expectedHotelId, bestValueResult.HotelId);
            Assert.Equal(expectedTotalPrice, bestValueResult.TotalPrice);
        }

        [Fact]
        public void Verify_Holiday_Search_Customer_3()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = DepartureAirportEnum.Any,
                TravellingTo = "LPA",
                DepartureDate = new DateOnly(2022, 11, 10),
                DurationNights = 14
            };

            const int expectedFlightId = 7;
            const int expectedHotelId = 6;
            const decimal expectedTotalPrice = 1175;

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            var bestValueResult = searchResults.FirstOrDefault();

            Assert.NotNull(bestValueResult);
            Assert.Equal(expectedFlightId, bestValueResult.FlightId);
            Assert.Equal(expectedHotelId, bestValueResult.HotelId);
            Assert.Equal(expectedTotalPrice, bestValueResult.TotalPrice);
        }

        [Fact]
        public void Verify_Holiday_Search_No_Results()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = DepartureAirportEnum.LTN,
                TravellingTo = "ACE",
                DepartureDate = new DateOnly(2024, 11, 10),
                DurationNights = 14
            };

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            var bestValueResult = searchResults.FirstOrDefault();

            Assert.Null(bestValueResult);
        }

        [Fact]
        public void Verify_Holiday_Search_Any_Departure_Airport()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = DepartureAirportEnum.Any,
                TravellingTo = "PMI",
                DepartureDate = new DateOnly(2023, 6, 15),
                DurationNights = 10
            };

            const int expectedFlightId = 6;
            const int expectedHotelId = 5;
            const decimal expectedTotalPrice = 675;

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            var bestValueResult = searchResults.FirstOrDefault();

            Assert.NotNull(bestValueResult);
            Assert.Equal(expectedFlightId, bestValueResult.FlightId);
            Assert.Equal(expectedHotelId, bestValueResult.HotelId);
            Assert.Equal(expectedTotalPrice, bestValueResult.TotalPrice);
        }

        [Fact]
        public void Verify_Holiday_Search_No_Hotel_Null_Result()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = DepartureAirportEnum.MAN,
                TravellingTo = "AGP",
                DepartureDate = new DateOnly(2023, 10, 25),
                DurationNights = 10
            };

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            var bestValueResult = searchResults.FirstOrDefault();

            Assert.Null(bestValueResult);
        }

        [Fact]
        public void Verify_Holiday_Search_No_Flight_Null_Result()
        {
            var searchCriteria = new HolidaySearchCriteria
            {
                DepartingFrom = DepartureAirportEnum.MAN,
                TravellingTo = "TFS",
                DepartureDate = new DateOnly(2022, 11, 5),
                DurationNights = 7
            };

            var searchResults = _holidaySearchService.HolidaySearch(searchCriteria).ToList();
            var bestValueResult = searchResults.FirstOrDefault();

            Assert.Null(bestValueResult);
        }
    }
}