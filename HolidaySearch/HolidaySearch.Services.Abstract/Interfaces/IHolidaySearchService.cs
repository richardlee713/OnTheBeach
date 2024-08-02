using HolidaySearch.Services.Abstract.Criteria;
using HolidaySearch.Services.Abstract.DTOs;

namespace HolidaySearch.Services.Abstract.Interfaces
{
    public interface IHolidaySearchService
    {
        IEnumerable<HolidayResultDto> HolidaySearch(HolidaySearchCriteria criteria);
    }
}
