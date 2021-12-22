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
        Task<Response<List<PersonDto>>> GetAllAsync();
        /// <summary>
        /// return contact by from id
        /// </summary>
        /// <param name="id">Person Id</param>
        /// <returns></returns>
        Task<Response<ReportRequestEvent>> CreateReportRequest(ReportRequestEvent reportRequestEvent);
        Task<Response<PersonDto>> GetByIdAsync(string id);
        Task<Response<PersonDto>> CreateAsync(PersonCreateDto personCreateDto);
        Task<Response<NoContent>> UpdateAsync(PersonUpdateDto personUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);

       
    }
}
