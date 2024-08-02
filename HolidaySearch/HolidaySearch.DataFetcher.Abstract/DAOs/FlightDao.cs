namespace HolidaySearch.DataFetcher.Abstract.DAOs
{
    public class FlightDao
    {
        public int Id { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Price { get; set; }
        public DateOnly Date { get; set; }
    }
}
