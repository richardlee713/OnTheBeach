using HolidaySearch.DataFetcher;
using HolidaySearch.DataFetcher.Abstract.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HolidaySearch.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IHolidayDataFetcher, HolidayDataFetcher>();
        }
    }
}
