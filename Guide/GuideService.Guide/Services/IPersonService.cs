using Guide.Shared.Dtos;
using Guide.Shared.Messages;
using GuideService.Guide.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuideService.Guide.Services
{
    public interface IPersonService
    {
        /// <summary>
        /// return all Contact list
        /// </summary>
        /// <returns></returns>
        Task<List<PersonDto>> GetAllAsync();
        /// <summary>
        /// return contact by from id
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns></returns>
        Task<ReportRequestEvent> CreateReportRequest(ReportRequestEvent reportRequestEvent);
        Task<List<ReportRequestEvent>> GetAllReportAsync();
        Task<PersonDto> GetByIdAsync(string id);
        Task CreateAsync(PersonCreateDto personCreateDto);
        Task<PersonUpdateDto> UpdateAsync(PersonUpdateDto personUpdateDto);
        Task<long> DeleteAsync(string id);

       
    }
}
