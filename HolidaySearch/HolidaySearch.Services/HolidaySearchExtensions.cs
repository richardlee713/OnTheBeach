using HolidaySearch.Services.Abstract.DTOs;

namespace HolidaySearch.Services
{
    public static class HolidaySearchExtensions
    {
        public static List<HolidayResultDto> CalculatePrices(this List<HolidayResultDto> searchResults)
        {
            searchResults.ForEach(sr =>
            {
                sr.HotelPrice = sr.HotelPricePerNight * sr.Duration;
                sr.TotalPrice = sr.HotelPrice + sr.FlightPrice;
            });

            return searchResults;
        }
    }
}
