﻿namespace HolidaySearch.Services.Abstract.Criteria
{
    public class HolidaySearchCriteria
    {
        public DepartureAirportEnum DepartingFrom { get; set; }
        public string TravellingTo { get; set; }
        public DateOnly DepartureDate { get; set; }
        public int DurationNights { get; set; }
    }
}
