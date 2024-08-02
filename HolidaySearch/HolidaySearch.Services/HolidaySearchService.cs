using HolidaySearch.Services.Abstract.Criteria;
using HolidaySearch.Services.Abstract.DTOs;
using HolidaySearch.Services.Abstract.Interfaces;

namespace HolidaySearch.Services
{
    public class HolidaySearchService : IHolidaySearchService
    {
        public IEnumerable<HolidayResultDto> HolidaySearch(HolidaySearchCriteria criteria)
        {
            return new List<HolidayResultDto>();
        }
    }
}
